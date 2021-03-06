﻿using App.Lib.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Lib.DAL.ADO
{
    public class CartaADO : ADOSuper
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public void Inserir(Carta entidade)
        {
            string SQL = @" INSERT INTO Carta (MateriaID, TextoFrente, TextoVerso, Status, Favorita) 
                                 VALUES (@MateriaID, @TextoFrente, @TextoVerso, @Status, @Favorita) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void Atualizar(Carta entidade)
        {
            string SQL = @"UPDATE Carta SET  
                                MateriaID = @MateriaID,
                                TextoFrente = @TextoFrente,
                                TextoVerso = @TextoVerso,
                                Status = @Status,
                                Favorita = @Favorita
                            WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public Carta Carregar(int id)
        {
            Carta entidade = null;
            string SQL = @"SELECT * FROM Carta c LEFT JOIN Materia m ON m.ID = c.MateriaID WHERE c.ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Carta, Materia, Carta>(SQL, (carta, materia) => { carta.Materia = materia; return carta; }, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        public IList<Carta> Listar()
        {
            IList<Carta> list = new List<Carta>();

            string SQL = @"SELECT * FROM Carta ORDER BY MateriaID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Carta>(SQL).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

        public IList<Carta> Listar(Int32 skip, Int32 take, string palavraChave)
        {

            IList<Carta> list = new List<Carta>();

            string SQLCount = @"SELECT COUNT(*) FROM Carta(NOLOCK) c LEFT JOIN Materia m ON m.ID = c.MateriaID WHERE c.TextoFrente LIKE @palavraChave AND m.Nome LIKE @palavrachave";

            string SQL = @"
            SELECT tbl.ID, tbl.TextoFrente, tbl.Favorita, tbl.Status, tbl.Favorita, tbl.MateriaID as ID, tbl.MateriaNome as Nome, tbl.MateriaCorTexto, tbl.MateriaCorBorda, tbl.MateriaCorFundo 
              FROM (SELECT ROW_NUMBER() OVER(ORDER BY m.ID) AS NUMBER,
                           c.ID, c.TextoFrente, c.Favorita, c.Status,
                           m.ID as MateriaID, m.Nome as MateriaNome, m.CorFundo as MateriaCorFundo, m.CorBorda as MateriaCorBorda, m.CorTexto as MateriaCorTexto 
                            FROM Carta (NOLOCK)  c
                            LEFT JOIN Materia (NOLOCK) m ON m.ID = c.MateriaID
                WHERE m.Nome like @palavraChave OR c.TextoFrente like @palavraChave) tbl
            WHERE NUMBER BETWEEN @Skip AND (@Skip+1 + @Take)
            ORDER BY tbl.MateriaNome, tbl.ID";



            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<Carta, Materia, Carta>(SQL, (carta, materia) =>
                {
                    carta.Materia = materia;
                    return carta;
                }, new
                {
                    Skip = skip,
                    Take = take,
                    palavraChave = "%" + palavraChave + "%"
                }).ToList();
                con.Close();
            }

            return list;
        }

        public void Deletar(int id)
        {
            string sql = @"DELETE FROM Carta WHERE ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                var transaction = con.BeginTransaction();
                con.Execute(sql, new { ID = id }, transaction);
                transaction.Commit();
                con.Close();
            }
        }

        public void Favoritar(int id, bool favorito)
        {
            string sql = @"UPDATE Carta SET favorita =@favorito WHERE ID = @ID";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(sql, new { ID = id, favorito = favorito });
                con.Close();
            }
        }
        public void MudarStatus(int id, bool status)
        {
            string sql = @"UPDATE Carta SET status =@status WHERE ID = @ID";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(sql, new { ID = id, status = status });
                con.Close();
            }
        }

        public Guid GerarSorteio(IList<int> ids, bool favoritas)
        {
            Random random = new Random();
            IList<Carta> retorno = null;
            string sql = @"SELECT c.ID FROM Carta c (nolock) LEFT JOIN Materia m ON c.MateriaID=m.ID WHERE c.Status = 'true' AND m.ID in @lista";
            sql = favoritas ? string.Format("{0} AND c.Favorita = 'true'", sql) : sql;
            Guid identificador = Guid.NewGuid();
            string sql2 = @"INSERT INTO Sorteio (CartaID, IdentificadorSorteio, Vista) VALUES (@CartaID, @IdentificadorSorteio, @Vista)";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                var transaction = con.BeginTransaction();
                var cartas = con.Query<int>(sql, new { lista = ids }, transaction).ToList();
                foreach (var c in cartas.OrderBy(i => random.Next()))
                {
                    con.Execute(sql2, new { CartaID = c, IdentificadorSorteio = identificador, Vista = false }, transaction);
                }
                transaction.Commit();
                con.Close();
            }
            return identificador;
        }
        
        public Sorteio Carregar(Guid identificador)
        {
            Sorteio retorno = new Sorteio();
            string sql = @"SELECT top 1 * FROM Sorteio s 
                            LEFT JOIN Carta c ON c.ID = s.CartaID 
                            LEFT JOIN Materia m ON m.ID = c.MateriaID
                            WHERE s.IdentificadorSorteio = @identificador AND s.Vista = 0 ORDER BY NEWID()";

            using (var con = _db.CreateConnection())
            {
                con.Open();
                retorno = con.Query<Sorteio, Carta, Materia, Sorteio>(sql, (sorteio, carta, materia) =>
                {
                    if (carta != null)
                        carta.Materia = materia;
                    sorteio.Carta = carta;
                    return sorteio;
                }, new { identificador = identificador }).FirstOrDefault();
                if (retorno != null)
                {
                    con.Execute("UPDATE Sorteio SET Vista = 1 WHERE ID = @ID", new { ID = retorno.ID });
                }
                else
                {
                    con.Execute("UPDATE Sorteio SET Vista = 0 WHERE IdentificadorSorteio = @id", new { id = identificador });  
                }
                con.Close();
            }
            return retorno;
        }

        public void ApagaSorteio(int id)
        {
            string sql = @"DELETE FROM Sorteio WHERE ID = @ID";
            using(var con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(sql, new { ID = id });
                con.Close();
 
            }
        }
    }
}

using App.Lib.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Lib.DAL.ADO
{
    public class MateriaADO : ADOSuper
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public void Inserir(Materia entidade)
        {
            string SQL = @" INSERT INTO Materia (Nome, CorTexto, CorFundo, CorBorda) 
                                 VALUES (@Nome, @CorTexto, @CorFundo, @CorBorda) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void Atualizar(Materia entidade)
        {
            string SQL = @"UPDATE Materia SET  
                                Nome = @Nome, 
                                CorBorda = @CorBorda, 
                                CorTexto = @CorTexto,  
                                CorFundo=@CorFundo
                            WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public Materia Carregar(int id)
        {
            Materia entidade = null;
            string SQL = @"SELECT * FROM Materia WHERE u.ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Materia>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }
        
        public IList<Materia> Listar()
        {
            IList<Materia> list = new List<Materia>();

            string SQL = @"SELECT * FROM Materia ORDER BY Nome";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Materia>(SQL).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

        public IList<Materia> Listar(Int32 skip, Int32 take, string palavraChave)
        {

            IList<Materia> list = new List<Materia>();

            string SQLCount = @"SELECT COUNT(*) FROM Materia(NOLOCK) WHERE Nome LIKE @palavraChave";

            //            // SQL SERVER 2008
            //            string SQL = @"
            //SELECT tbl.ID, tbl.Nome, tbl.Login, tbl.Senha, tbl.UltimoAcesso, tbl.Inativo, tbl.DataCadastro, tbl.Email, tbl.UsuarioPerfilID, tbl.UsuarioID,
            //       tbl.perfilID as ID, tbl.perfilNomeDB as NomeDB, tbl.perfilDescricao as Descricao 
            //  FROM (SELECT ROW_NUMBER() OVER(ORDER BY u.ID) AS NUMBER,
            //               u.ID, u.Nome, u.Login, u.Senha, u.UltimoAcesso, u.Inativo, u.DataCadastro, u.Email, u.UsuarioPerfilID, u.UsuarioID,
            //               p.ID as perfilID, p.Nome as perfilNomeDB, p.Descricao as perfilDescricao
            //          FROM Usuario (NOLOCK)  u
            //    INNER JOIN UsuarioPerfil (NOLOCK) p ON u.UsuarioPerfilID = p.ID
            //    WHERE u.nome like @palavraChave " + (somenteAtivos ? " AND Inativo = 0" : "") + @"
            //) tbl
            //WHERE NUMBER BETWEEN @Skip AND (@Skip + @Take)
            //ORDER BY tbl.perfilNomeDB, tbl.Login";

            string SQL = @"SELECT * FROM Materia
                                    WHERE Nome like @palavraChave
                                    ORDER Nome
                                    DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<Materia>(SQL, new {
                    Skip = skip,
                    Take = take,
                    palavraChave = "%" + palavraChave + "%"
                }).ToList();
                con.Close();
            }

            return list;
        }


    }
}

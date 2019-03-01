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
            string SQL = @"SELECT * FROM Materia WHERE ID=@ID";

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

            string SQL = @"
            SELECT tbl.ID, tbl.CorFundo, tbl.CorBorda, tbl.CorTexto, tbl.Nome 
              FROM (SELECT ROW_NUMBER() OVER(ORDER BY m.ID) AS NUMBER,
                           m.ID, m.Nome, m.CorFundo, m.CorBorda, m.CorTexto FROM Materia (NOLOCK)  m
                WHERE m.Nome like @palavraChave) tbl
            WHERE NUMBER BETWEEN @Skip AND (@Skip+1 + @Take)
            ORDER BY tbl.Nome";



            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<Materia>(SQL, new
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
            string sql = @"DELETE FROM Materia WHERE ID = @ID";
            string sql2 = @"DELETE FROM FlashCard WHERE MateriaID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                var transaction = con.BeginTransaction();
                con.Execute(sql2, new { ID = id }, transaction);
                con.Execute(sql, new { ID = id }, transaction);
                transaction.Commit();
                con.Close();
            }
        }

    }
}

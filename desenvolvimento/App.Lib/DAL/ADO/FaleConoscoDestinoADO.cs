using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BradescoNext.Lib.Entity;
using Dapper;
using System.Data.Common;

namespace BradescoNext.Lib.DAL.ADO
{
    public class FaleConoscoDestinoADO : ADOSuper, IFaleConoscoDestinoDAL
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        /// <summary> 
        /// Inclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public void Inserir(FaleConoscoDestino entidade)
        {
            string SQL = @" INSERT INTO FaleConoscoDestino (Email, Nome) 
                                 VALUES ( @Email, @Nome) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary> 
        /// Método que atualiza os dados entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        public void Atualizar(FaleConoscoDestino entidade)
        {
            string SQL = @"UPDATE FaleConoscoDestino SET  
                                Email=@Email,
                                Nome=@Nome
                          WHERE ID=@ID ";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary>
        /// Carrega FaleConoscoDestino pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FaleConoscoDestino Carregar(int id)
        {
            FaleConoscoDestino entidade = null;
            string SQL = @"SELECT TOP 1 ID, 
                                        Email, 
                                        Nome
                            FROM FaleConoscoDestino 
                           WHERE ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<FaleConoscoDestino>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Carrega lista de destinos
        /// </summary>
        /// <param name="faleConoscoAssuntoID"></param>
        /// <returns></returns>
        public IList<FaleConoscoDestino> Listar()
        {
            IList<FaleConoscoDestino> list = null;

            string SQL = @"SELECT fcd.ID, fcd.Email, fcd.Nome
                             FROM FaleConoscoDestino (NOLOCK) fcd ORDER BY fcd.Email ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<FaleConoscoDestino>(SQL).ToList();
                TotalRegistros = list.Count;

                con.Close();

            }

            return list;
        }

        /// <summary>
        /// Carrega lista de destinos de determinado assunto e página
        /// </summary>
        /// <param name="faleConoscoAssuntoID"></param>
        /// <returns></returns>
        public IList<FaleConoscoDestino> Listar(Int32 skip, Int32 take)
        {
            IList<FaleConoscoDestino> list = null;

            
            string SQLCount = @"SELECT COUNT(*) FROM FaleConoscoDesntino";
            string SQL = @"SELECT fcd.ID, fcd.Email, fcd.Nome
                    FROM FaleConoscoDestino (NOLOCK) fcd
                    ORDER BY fcd.Email ASC
                  OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount).FirstOrDefault();

                list = con.Query<FaleConoscoDestino>(SQL, new { Skip = skip, Take = take }).ToList();
                con.Close();
                

            }

            return list;
        }

    }
}

using System;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using BradescoNext.Lib.DAL;
using BradescoNext.Lib.Entity;
using Dapper;
using System.Linq;

namespace BradescoNext.Lib.DAL.ADO
{

    public class FaleConoscoADO : ADOSuper, IFaleConoscoDAL
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
        public void Inserir(FaleConosco entidade)
        {
            string SQL = @" INSERT INTO FaleConosco (Nome, EmailCrt, FaleConoscoAssuntoID, Mensagem, DataCadastro) 
                            VALUES (@Nome, " + Criptografar(entidade.Email) + ", @FaleConoscoAssuntoID, @Mensagem, @DataCadastro) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary>
        /// Método que carrega um FaleConosco a partir de seu id
        /// </summary>
        /// <returns></returns>
        public FaleConosco Carregar(int id)
        {
            FaleConosco entidade = null;
            string SQL = @"SELECT TOP 1 fc.ID, 
                                        " + Decriptografar("fc.EmailCrt") + @" as Email, 
                                        fc.Nome, 
                                        fc.FaleConoscoAssuntoID,
                                        fc.Mensagem, 
                                        fc.DataCadastro,
                                        fa.ID, fa.Nome
                           FROM FaleConosco fc, FaleConoscoAssunto fa
                           WHERE fc.ID=@ID
                           AND fc.FaleConoscoAssuntoID = fa.ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<FaleConosco, FaleConoscoAssunto, FaleConosco>(SQL, (faleConosco, faleConoscoAssunto) =>
                {
                    faleConosco.FaleConoscoAssunto = faleConoscoAssunto;
                    return faleConosco;
                }, new
                {
                    ID = id
                }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de newsletter
        /// </summary>
        /// <returns></returns>
        public IList<FaleConosco> Listar()
        {
            IList<FaleConosco> list = new List<FaleConosco>();

            string SQL = @"SELECT 
                        fc.ID,
                        fc.Nome, 
                        " + Decriptografar("fc.EmailCrt") + @" as Email, 
                        fc.FaleConoscoAssuntoID, 
                        fc.DataCadastro, 
                        fa.ID, fa.Nome
                    FROM FaleConosco fc
                    INNER JOIN FaleConoscoAssunto fa ON fc.FaleConoscoAssuntoID = fa.ID
                    ORDER BY fa.Nome, fc.DataCadastro";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<FaleConosco, FaleConoscoAssunto, FaleConosco>(SQL, (faleConosco, faleConoscoAssunto) =>
                {
                    faleConosco.FaleConoscoAssunto = faleConoscoAssunto;
                    return faleConosco;
                }).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

        /// <summary>
        /// Método que retorna a lista de FaleConosco de uma página
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegPorPagina"></param>
        /// <returns></returns>
        public IList<FaleConosco> Listar(Int32 skip, Int32 take, string palavraChave)
        {           

            IList<FaleConosco> list = new List<FaleConosco>();

            string SQLCount = @"SELECT COUNT(*) 
                                 FROM FaleConosco fc
                           INNER JOIN FaleConoscoAssunto fa ON fc.FaleConoscoAssuntoID = fa.ID
                                WHERE fc.Nome like @palavraChave or fa.Nome like @palavraChave";
            string SQL = @"SELECT 
                        fc.ID,
                        fc.Nome, 
                        " + Decriptografar("fc.EmailCrt") + @" as Email, 
                        fc.FaleConoscoAssuntoID, 
                        fc.DataCadastro, 
                        fa.ID, fa.Nome
                    FROM FaleConosco fc
                    INNER JOIN FaleConoscoAssunto fa ON fc.FaleConoscoAssuntoID = fa.ID
                    WHERE fc.Nome like @palavraChave or fa.Nome like @palavraChave
                    ORDER BY fa.Nome, fc.DataCadastro
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                    
                list = con.Query<FaleConosco, FaleConoscoAssunto, FaleConosco>(SQL, (faleConosco, faleConoscoAssunto) =>
                {
                    faleConosco.FaleConoscoAssunto = faleConoscoAssunto;
                    return faleConosco;
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
		
    }
}
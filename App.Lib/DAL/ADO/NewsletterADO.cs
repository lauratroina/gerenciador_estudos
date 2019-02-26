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
using BradescoNext.Lib.Entity.Enumerator;
using BradescoNext.Lib.Util;
using BradescoNext.Lib.Enumerator;


namespace BradescoNext.Lib.DAL.ADO
{

    public class NewsletterADO : ADOSuper, INewsletterDAL
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
        public void Inserir(Newsletter entidade)
        {
            string SQL = @" INSERT INTO Newsletter (EmailCrt, Origem, Inativo, DataCadastro) 
                                 VALUES (" + Criptografar(entidade.Email) + @", @OrigemDB, @Inativo, @DataCadastro) ";

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
        public void Atualizar(Newsletter entidade)
        {

            string SQL = @"UPDATE Newsletter SET  
                                EmailCrt=" + Criptografar(entidade.Email) + @",
                                Origem=@OrigemDB,
                                Inativo=@Inativo,
                                DataCadastro=@DataCadastro
                          WHERE ID=@ID ";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary>
        /// Carrega Newsletter pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Newsletter Carregar(int id)
        {

            Newsletter entidade = null;
            string SQL = @"SELECT TOP 1 ID, " + 
                                        Decriptografar("EmailCrt") + @" as Email, 
                                        Origem as OrigemDB, 
                                        Inativo,
                                        DataCadastro
                            FROM Newsletter 
                           WHERE ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Newsletter>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Carrega a Newsletter através do Email e da Origem
        /// </summary>
        /// <param name="email"></param>
        /// <param name="origem"></param>
        /// <returns></returns>
        public Newsletter Carregar(string email, enumNewsletterOrigem origem)
        {

            Newsletter entidade = null;
            string SQL = @"SELECT TOP 1 ID, " +
                                        Decriptografar("EmailCrt") + @" as Email, 
                                        Origem as OrigemDB, 
                                        Inativo,
                                        DataCadastro
                            FROM Newsletter 
                           WHERE " + Decriptografar("EmailCrt") + @" = @Email AND Origem = @Origem";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Newsletter>(SQL, new { 
                    Email = email, 
                    Origem = origem.ValueAsString()
                }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de newsletter
        /// </summary>
        /// <returns></returns>
        public IList<Newsletter> Listar(bool somenteAtivos = true)
        {
            IList<Newsletter> list = null;
            List<string> lstWhere = new List<string>();
            if (somenteAtivos)
            {
                lstWhere.Add("Inativo = 0");
            }
            string where = (lstWhere.Count == 0) ? "" : " WHERE " + string.Join(" AND ", lstWhere);


            string SQL = @"SELECT ID, "+
                                  Decriptografar("EmailCrt") + @" as Email, 
                                  Origem as OrigemDB, 
                                  Inativo,
                                  DataCadastro
                             FROM Newsletter " + where + " ORDER BY " + Decriptografar("EmailCrt") + " ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Newsletter>(SQL).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

        /// <summary>
        /// Método que retorna a lista de newsletter de uma página
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegPorPagina"></param>
        /// <returns></returns>
        public IList<Newsletter> Listar(Int32 skip, Int32 take, string palavraChave, bool somenteAtivos, string origem)
        {
            
            IList<Newsletter> list = new List<Newsletter>();

            List<string> lstWhere = new List<string>();
            if (somenteAtivos)
            {
                lstWhere.Add("Inativo = 0");
            }

            lstWhere.Add("Origem = '"+origem+"'");

            if(!string.IsNullOrEmpty(palavraChave))
            {
                lstWhere.Add(Decriptografar("EmailCrt") + " LIKE @palavraChave ");
            }
            
            string where = (lstWhere.Count == 0) ? "" : " WHERE " + string.Join(" AND ", lstWhere);

            string SQLCount = @"SELECT COUNT(*) FROM Newsletter " + where;

            string SQL = @"SELECT 
                            ID, " +
                            Decriptografar("EmailCrt") + @" as Email, 
                            Origem as OrigemDB, 
                            Inativo,
                            DataCadastro
                    FROM Newsletter " + where + @"
                ORDER BY " + Decriptografar("EmailCrt") + " ASC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<Newsletter>(SQL, new { Skip = skip, Take = take , palavraChave = "%" + palavraChave +"%"}).ToList();

                con.Close();
                
            }

            return list;
        }
    }
}
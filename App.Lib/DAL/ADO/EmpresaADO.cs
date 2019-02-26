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
using System.Linq;
using Dapper;


namespace BradescoNext.Lib.DAL.ADO
{

    public class EmpresaADO : ADOSuper, IEmpresaDAL
    {

        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        /// <summary>
        /// Método que insere um usuário na base de dados
        /// </summary>
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        /// <returns></returns>
        public void Inserir(Empresa entidade)
        {
            string SQL = @" INSERT INTO Empresa (Nome, Inativo) 
                                 VALUES (@Nome, @Inativo) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary>
        /// Método que exclui uma empresa na base de dados
        /// </summary>
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        /// <returns></returns>
        public void Excluir(int id)
        {
            string SQL = @" DELETE FROM Empresa WHERE ID = @ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        /// <summary>
        /// Método que atualiza os dados de um usuário na base de dados através de seu ID
        /// </summary>
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        /// <returns></returns>
        public void Atualizar(Empresa entidade)
        {
            string SQL = @"UPDATE Empresa SET  
                                Nome = @Nome, 
                                Inativo = @Inativo
                          WHERE ID=@ID ";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }
        /// <summary>
        /// Método que carrega os dados de um usuário através de seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Empresa Carregar(int id)
        {
            Empresa entidade = null;
            string SQL = @"SELECT TOP 1 
                                ID, Nome, Inativo
                            FROM Empresa
                           WHERE ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Empresa>(SQL, new
                {
                    ID = id
                }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de empresas
        /// </summary>
        /// <returns></returns>
        public IList<Empresa> Listar()
        {
            IList<Empresa> list = new List<Empresa>();

            string SQL = @"SELECT * FROM Empresa ORDER BY Nome";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Empresa>(SQL).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

        /// <summary>
        /// Método que retorna a lista de usuarios de uma página
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegPorPagina"></param>
        /// <returns></returns>
        public IList<Empresa> Listar(Int32 skip, Int32 take, string palavraChave, bool somenteAtivos = true)
        {

            IList<Empresa> list = new List<Empresa>();

            string SQLCount = @"SELECT COUNT(*) FROM Empresa WHERE nome LIKE @palavraChave ";

            string SQL = @"SELECT ID, Nome, Inativo
                                    FROM Empresa
                                    WHERE Nome like @palavraChave
                                    ORDER BY Nome
                                    DESC OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<Empresa>(SQL, new { 
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
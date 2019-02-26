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

    public class EstudoCasoGraficoADO : ADOSuper, IEstudoCasoGraficoDAL
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
        public void Inserir(EstudoCasoGrafico entidade)
        {
            string SQL = @"INSERT 
                             INTO EstudoCasoGrafico 
                                  (EstudoCasoID,
                                   Titulo,
                                   SubTitulo,
                                   Q1Titulo,
                                   Q1SubTitulo,
                                   Q1Descricao,
                                   Q1Imagem,
                                   Q2Percentagem,
                                   Q2Descricao,
                                   Q3Titulo,
                                   Q3SubTitulo,
                                   Q3Descricao,
                                   Q4Titulo,
                                   Q4SubTitulo,
                                   Q4Descricao,
                                   Widget) 
                           VALUES (@EstudoCasoID,
                                   @Titulo,
                                   @SubTitulo,
                                   @Q1Titulo,
                                   @Q1SubTitulo,
                                   @Q1Descricao,
                                   @Q1Imagem,
                                   @Q2Percentagem,
                                   @Q2Descricao,
                                   @Q3Titulo,
                                   @Q3SubTitulo,
                                   @Q3Descricao,
                                   @Q4Titulo,
                                   @Q4SubTitulo,
                                   @Q4Descricao,
                                   @Widget);";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary>
        /// Método que atualiza os dados de um usuário na base de dados através de seu ID
        /// </summary>
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        /// <returns></returns>
        public void Atualizar(EstudoCasoGrafico entidade)
        {
            string SQL = @"UPDATE EstudoCasoGrafico
                              SET UsuarioSiteID = @EstudoCasoID,
                                  Titulo        = @Titulo,
                                  SubTitulo     = @SubTitulo,
                                  Q1Titulo      = @Q1Titulo,
                                  Q1SubTitulo   = @Q1SubTitulo,
                                  Q1Descricao   = @Q1Descricao,
                                  Q1Imagem      = @Q1Imagem,
                                  Q2Percentagem = @Q2Percentagem,
                                  Q2Descricao   = @Q2Descricao,
                                  Q3Titulo      = @Q3Titulo,
                                  Q3SubTitulo   = @Q3SubTitulo,
                                  Q3Descricao   = @Q3Descricao,
                                  Q4Titulo      = @Q4Titulo,
                                  Q4SubTitulo   = @Q4SubTitulo,
                                  Q4Descricao   = @Q4Descricao,
                                  Widget        = @Widget
                            WHERE ID=@ID;";


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
        public EstudoCasoGrafico Carregar(int id)
        {
            EstudoCasoGrafico entidade = null;
            string SQL = @"SELECT TOP 1
                                  ID,
                                  EstudoCasoID,
                                  Titulo,
                                  SubTitulo,
                                  Q1Titulo,
                                  Q1SubTitulo,
                                  Q1Descricao,
                                  Q1Imagem,
                                  Q2Percentagem,
                                  Q2Descricao,
                                  Q3Titulo,
                                  Q3SubTitulo,
                                  Q3Descricao,
                                  Q4Titulo,
                                  Q4SubTitulo,
                                  Q4Descricao,
                                  Widget
                             FROM EstudoCasoGrafico (NOLOCK)
                            WHERE ID=@ID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade = con.Query<EstudoCasoGrafico>(SQL, new { ID = id }).FirstOrDefault();
                con.Close();
            }
            return entidade;
        }

        public EstudoCasoGrafico CarregarParaEstudo(int estudoCasoID)
        {
            EstudoCasoGrafico entidade = null;
            string SQL = @"SELECT TOP 1
                                  ID,
                                  EstudoCasoID,
                                  Titulo,
                                  SubTitulo,
                                  Q1Titulo,
                                  Q1SubTitulo,
                                  Q1Descricao,
                                  Q1Imagem,
                                  Q2Percentagem,
                                  Q2Descricao,
                                  Q3Titulo,
                                  Q3SubTitulo,
                                  Q3Descricao,
                                  Q4Titulo,
                                  Q4SubTitulo,
                                  Q4Descricao,
                                  Widget
                             FROM EstudoCasoGrafico (NOLOCK)
                            WHERE EstudoCasoID=@estudoCasoID;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                entidade = con.Query<EstudoCasoGrafico>(SQL, new { EstudoCasoID = estudoCasoID }).FirstOrDefault();
                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de usuarios
        /// </summary>
        /// <returns></returns>
        public IList<EstudoCasoGrafico> Listar()
        {
            IList<EstudoCasoGrafico> list = new List<EstudoCasoGrafico>();

            string SQL = @"SELECT ID,
                                  EstudoCasoID,
                                  Titulo,
                                  SubTitulo,
                                  Q1Titulo,
                                  Q1SubTitulo,
                                  Q1Descricao,
                                  Q1Imagem,
                                  Q2Percentagem,
                                  Q2Descricao,
                                  Q3Titulo,
                                  Q3SubTitulo,
                                  Q3Descricao,
                                  Q4Titulo,
                                  Q4SubTitulo,
                                  Q4Descricao,
                                  Widget
                             FROM EstudoCasoGrafico (NOLOCK)
                            ORDER BY Titulo";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                list = con.Query<EstudoCasoGrafico>(SQL).ToList();
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
        public IList<EstudoCasoGrafico> Listar(Int32 skip, Int32 take, string palavraChave)
        {
            IList<EstudoCasoGrafico> list = new List<EstudoCasoGrafico>();

            string SQLCount = @"SELECT COUNT(*) FROM EstudoCasoGrafico WHERE Titulo LIKE @palavraChave ";

            string SQL = @"SELECT ID,
                                  EstudoCasoID,
                                  Titulo,
                                  SubTitulo,
                                  Q1Titulo,
                                  Q1SubTitulo,
                                  Q1Descricao,
                                  Q1Imagem,
                                  Q2Percentagem,
                                  Q2Descricao,
                                  Q3Titulo,
                                  Q3SubTitulo,
                                  Q3Descricao,
                                  Q4Titulo,
                                  Q4SubTitulo,
                                  Q4Descricao,
                                  Widget
                             FROM EstudoCasoGrafico (NOLOCK)
                            WHERE Titulo LIKE @palavraChave
                            ORDER BY Titulo ASC 
                           OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY;";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<EstudoCasoGrafico>(SQL, new { Skip = skip, Take = take, palavraChave = "%" + palavraChave + "%" }).ToList();
                con.Close();
            }

            return list;
        }


    }
}
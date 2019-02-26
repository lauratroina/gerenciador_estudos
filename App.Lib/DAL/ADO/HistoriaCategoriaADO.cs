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

namespace BradescoNext.Lib.DAL.ADO
{

    public class HistoriaCategoriaADO : ADOSuper, IHistoriaCategoriaDAL
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
        public void Inserir(HistoriaCategoria entidade)
        {
			string SQL = @" INSERT INTO HistoriaCategoria  (Nome, Inativo, Ordem, CorLabel, CorFont, Valor)
                            VALUES (@Nome, @Inativo, ISNULL((SELECT MAX(Ordem)+1 FROM HistoriaCategoria), 1), @CorLabel, @CorFont, @Valor)";
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
        public void Atualizar(HistoriaCategoria entidade)
        {
            string SQL = @"UPDATE HistoriaCategoria SET  
                                Inativo = @Inativo,
                                Nome = @Nome,
                                Ordem = @Ordem,
                                CorLabel = @CorLabel,
                                CorFont = @CorFont,
                                Valor = @Valor
                          WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }


        /// <summary> 
        /// Método que carrega uma entidade. 
        /// </summary> 
        /// <param name="entidade">Entidade a ser carregada (somente o identificador é necessário).</param> 
        /// <returns></returns> 
        public HistoriaCategoria Carregar(int id)
        {
            HistoriaCategoria entidade = null;
            string SQL = @"SELECT TOP 1 ID, 
                                        Nome, 
                                        Inativo, 
                                        Ordem,
                                        Valor,
                                        CorLabel,
                                        CorFont
                            FROM HistoriaCategoria 
                           WHERE ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<HistoriaCategoria>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

		 /// <summary> 
        /// Método que retorna todas as entidades. 
        /// </summary> 
		public IList<HistoriaCategoria> Listar() 
        {
            IList<HistoriaCategoria> list = new List<HistoriaCategoria>();
            string SQL = @"SELECT ID, Nome, Inativo, Ordem, Valor, CorLabel, CorFont
                                 FROM HistoriaCategoria ORDER BY Ordem ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<HistoriaCategoria>(SQL).ToList();
                TotalRegistros = list.Count;

                con.Close();

            }

            return list;
        }

        public IList<HistoriaCategoria> Listar(int indicadoID)
        {
            IList<HistoriaCategoria> list = new List<HistoriaCategoria>();

            string SQLDados = @"SELECT DISTINCT 
                                HC.ID,
                                HC.Nome,
                                HC.CorLabel,
                                HC.CorFont
	                            FROM Historia (NOLOCK) H
                                INNER JOIN HistoriaCategoria (NOLOCK) HC ON H.HistoriaCategoriaID = HC.ID
                                WHERE H.IndicadoID = @IndicadoID
                                   ORDER BY Nome";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                list = con.Query<HistoriaCategoria>(SQLDados,
                    new
                    {
                        IndicadoID = indicadoID,
                        TriagemAprovacao = enumAprovacao.aprovado.ValueAsString(),
                        TriagemAprovacaoRessalva = enumAprovacao.aprovadoComRessalva.ValueAsString()
                    }).ToList();
                con.Close();
            }

            return list;
        }

        public IList<HistoriaCategoria> Listar(bool somenteAtivos)
        {
            IList<HistoriaCategoria> list = new List<HistoriaCategoria>();

            string SQLCount = @"SELECT COUNT(*) FROM HistoriaCategoria " + ((somenteAtivos) ? " WHERE Inativo = 0 " : "");
            string SQL = @"SELECT ID, Nome, Inativo, Ordem, Valor, CorLabel, CorFont
                    FROM HistoriaCategoria " + 
                    ((somenteAtivos) ? " WHERE Inativo = 0 " : "") + 
                    "ORDER BY Ordem ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                try
                {
                    con.Open();

                    TotalRegistros = con.Query<int>(SQLCount, new { Inativo = !somenteAtivos }).FirstOrDefault();

                    list = con.Query<HistoriaCategoria>(SQL, new { Inativo = !somenteAtivos }).ToList();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return list;
        }

        public IList<HistoriaCategoria> ListaCategoriasDaHistoria(int historiaID)
        {
            IList<HistoriaCategoria> list = new List<HistoriaCategoria>();

            string SQL = @"SELECT ID
                    FROM HistoriaCategoriaHistoria 
                    WHERE HistoriaID = @HistoriaID";

            using (DbConnection con = _db.CreateConnection())
            {
                try
                {
                    con.Open();

                    list = con.Query<HistoriaCategoria>(SQL, new { HistoriaID = historiaID }).ToList();
                    con.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }

            return list;
        }
        /// <summary> 
        /// Método que retorna todas as entidades. 
		/// SELECT * FROM HistoriaCategoria ORDER BY ID DESC 
        /// </summary> 
        public IList<HistoriaCategoria> Listar(Int32 skip, Int32 take, string palavraChave) 
        {
            IList<HistoriaCategoria> list = new List<HistoriaCategoria>();


            string SQLCount = @"SELECT COUNT(*) FROM HistoriaCategoria WHERE (Nome like @palavraChave)";
            string SQL = @"SELECT ID, Nome, Inativo, Ordem, Valor, CorLabel, CorFont
                    FROM HistoriaCategoria 
                    WHERE (Nome like @palavraChave)
                    ORDER BY Ordem ASC
                    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();

                list = con.Query<HistoriaCategoria>(SQL, new { Skip = skip, Take = take, palavraChave = "%" + palavraChave + "%" }).ToList();
                con.Close();

            }

            return list;
        }
		
    }
}
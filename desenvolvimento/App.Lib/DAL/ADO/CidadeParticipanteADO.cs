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
using BradescoNext.Lib.Models;
using BradescoNext.Lib.Util;


namespace BradescoNext.Lib.DAL.ADO
{

    public class CidadeParticipanteADO : ADOSuper, ICidadeParticipanteDAL
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
        public void Inserir(CidadeParticipante entidade)
        {
            string SQL = @" INSERT INTO CidadeParticipante     
                                     (CidadeID,QuantidadeLugares,QuantidadeLugaresInterno,ExibeMapa,DataModificacao,UsuarioID,Inativo,RealocacaoPendente,RealocacaoPendenteInterno) 
                                      VALUES    
                                     (@CidadeID,@QuantidadeLugares,@QuantidadeLugaresInterno,@ExibeMapa,@DataModificacao,@UsuarioID,@Inativo,@RealocacaoPendente,@RealocacaoPendenteInterno) ";

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
        public void Atualizar(CidadeParticipante entidade)
        {
            string SQL = @"UPDATE CidadeParticipante SET  
                                    CidadeID=@CidadeID,
                                    QuantidadeLugares=@QuantidadeLugares,
                                    QuantidadeLugaresInterno=@QuantidadeLugaresInterno,
                                    ExibeMapa=@ExibeMapa,
                                    DataModificacao=@DataModificacao,
                                    UsuarioID=@UsuarioID,
                                    Inativo=@Inativo,
                                    RealocacaoPendente=@RealocacaoPendente,
                                    RealocacaoPendenteInterno=@RealocacaoPendenteInterno
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
        public CidadeParticipante Carregar(int id, bool condutor = false)
        {
            CidadeParticipante entidadeRetorno = null;

            string SQL = @"SELECT cp.ID, 
								  cp.QuantidadeLugaresInterno, 
								  cp.QuantidadeLugares, 
                                  cp.ExibeMapa,  
                                  cp.Inativo,  
                                  cp.CidadeID,
                                  cp.RealocacaoPendente,  
                                  cp.RealocacaoPendenteInterno,
                                  (SELECT COUNT(*) FROM Indicado (NOLOCK) WHERE CidadeParticipanteID = cp.ID " + (condutor ? " AND Condutor = 1" : "") + @") as QuantidadeIndicados,
                                  (SELECT COUNT(*) FROM IndicadoInterno (NOLOCK) WHERE CidadeParticipanteID = cp.ID AND Inativo = 0 " + (condutor ? " AND Condutor = 1" : "") + @") as QuantidadeIndicadosInterno,  
                                  c.ID, c.Nome, 
								  e.ID, e.Nome
                             FROM CidadeParticipante (NOLOCK) cp
							INNER JOIN Cidade (NOLOCK) c ON cp.CidadeID = c.ID
                            INNER JOIN Estado (NOLOCK) e ON c.EstadoID = e.ID 
                            WHERE cp.ID = @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                entidadeRetorno = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL, (cidadeParticipante, cidade, estado) =>
                {

                    cidadeParticipante.Cidade = cidade;
                    cidadeParticipante.Cidade.Estado = estado;
                    return cidadeParticipante;
                }, new { ID = id }).FirstOrDefault();

                con.Close();
                
            }

            return entidadeRetorno;

        }

        /// <summary> 
        /// Método que retorna todas as entidades. 
        /// SELECT * FROM CidadeParticipante ORDER BY ID DESC 
        /// </summary> 
        public IList<CidadeParticipante> Listar()
        {
            IList<CidadeParticipante> list = new List<CidadeParticipante>();

            string SQL = @"SELECT CP.ID 
                            ,C.ID
                            ,C.Nome
                            ,E.ID
                            ,E.UF
                            FROM CidadeParticipante (NOLOCK) CP
                            INNER JOIN Cidade (NOLOCK) C ON C.ID = CP.CidadeID
                            INNER JOIN Estado (NOLOCK) E ON E.ID = C.EstadoID
                            ORDER BY E.UF, C.Nome";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL,
                    (cidadeParticipante, cidade, estado) =>
                    {
                        cidadeParticipante.Cidade = cidade;
                        cidadeParticipante.Cidade.Estado = estado;
                        return cidadeParticipante;
                    },
                    new { Inativo = false }
                    ).ToList();

                con.Close();

            }

            return list;
        }

        public IList<CidadeParticipante> ListarAtivas()
        {
            IList<CidadeParticipante> list = new List<CidadeParticipante>();

            string SQL = @"SELECT 
                            CP.ID 
                            ,C.ID
                            ,C.Nome
                            ,E.ID
                            ,E.UF
                            FROM CidadeParticipante (NOLOCK) CP
                            INNER JOIN Cidade (NOLOCK) C ON C.ID = CP.CidadeID
                            INNER JOIN Estado (NOLOCK) E ON E.ID = C.EstadoID
                            WHERE CP.Inativo = 0
                            ORDER BY E.UF, C.Nome";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL, (cidadeP, cidade, estado) =>
                {
                    cidadeP.Cidade = cidade;
                    cidadeP.Cidade.Estado = estado;
                    return cidadeP;
                }).ToList();

                con.Close();

            }

            return list;
        }

        /// <summary>
        /// Lista Cidades Participantes
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="estadoID"></param>
        /// <param name="palavraChave"></param>
        /// <returns></returns>
        public IList<CidadeParticipante> Listar(Int32 skip, Int32 take, string palavraChave, bool realocacaoPendente, bool realocacaoPendenteInterno)
        {

            IList<CidadeParticipante> list = new List<CidadeParticipante>();

            List<string> lstWhere = new List<string>();
            if (!string.IsNullOrEmpty(palavraChave))
            {
                lstWhere.Add("(c.Nome like @palavraChave OR e.Nome like @palavraChave)");
            }
            if ((realocacaoPendente) && (realocacaoPendenteInterno))
            {
                lstWhere.Add("(cp.RealocacaoPendente = 1 OR cp.RealocacaoPendenteInterno = 1)");
            }
            else if (realocacaoPendente)
            {
                lstWhere.Add("cp.RealocacaoPendente = 1");
            }
            else if (realocacaoPendenteInterno)
            {
                lstWhere.Add("cp.RealocacaoPendenteInterno = 1");
            }
            string where = (lstWhere.Count == 0) ? "" : " WHERE " + string.Join(" AND ", lstWhere);

            string SQLCount = @"SELECT COUNT(*) 
                                  FROM Cidade c
                            INNER JOIN CidadeParticipante (NOLOCK) cp ON cp.CidadeID = c.ID
                            INNER JOIN Estado (NOLOCK) e ON c.EstadoID = e.ID "+
                                 where;

            string SQL = @"SELECT cp.ID, 
								  cp.QuantidadeLugaresInterno, 
								  cp.QuantidadeLugares, 
                                  cp.Inativo,
                                  cp.RealocacaoPendente,  
                                  cp.RealocacaoPendenteInterno,
                                  (SELECT COUNT(*) FROM Indicado (NOLOCK) WHERE CidadeParticipanteID = cp.ID) as QuantidadeIndicados,
                                  (SELECT COUNT(*) FROM IndicadoInterno (NOLOCK) WHERE CidadeParticipanteID = cp.ID AND Inativo = 0) as QuantidadeIndicadosInterno,  
                                  c.ID, c.Nome, 
								  e.ID, e.Nome
							FROM CidadeParticipante (NOLOCK) cp
							INNER JOIN Cidade (NOLOCK) c ON cp.CidadeID = c.ID
                            INNER JOIN Estado (NOLOCK) e ON c.EstadoID = e.ID  " +
                            where + @" ORDER BY e.Nome, c.Nome
                            OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%"}).FirstOrDefault();

                list = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL, 
                    (cidadeParticipante, cidade, estado) =>
                {

                    cidadeParticipante.Cidade = cidade;
                    cidadeParticipante.Cidade.Estado = estado;
                    return cidadeParticipante;
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

        public void InserirLog(int id)
        {
            string SQL = @" INSERT INTO LogCidadeParticipante (CidadeParticipanteID,CidadeID,QuantidadeLugares,QuantidadeLugaresInterno,ExibeMapa,DataModificacao,UsuarioID,Inativo,RealocacaoPendente,RealocacaoPendenteInterno) 
                                     (SELECT ID as CidadeParticipanteID,CidadeID,QuantidadeLugares,QuantidadeLugaresInterno,ExibeMapa,DataModificacao,UsuarioID,Inativo,RealocacaoPendente,RealocacaoPendenteInterno
                                      FROM CidadeParticipante
                                      WHERE ID = @ID) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { ID = id });
                con.Close();
            }
        }

        public CidadeRealocacaoPendente QuantidadeRealocacaoPendente()
        {

            CidadeRealocacaoPendente retorno;
            string SQL = @"SELECT SUM(IIF(RealocacaoPendente = 1, 1, 0)) as Quantidade,
                                  SUM(IIF(RealocacaoPendenteInterno = 1, 1, 0)) as QuantidadeInterno,
                                  SUM(IIF(RealocacaoPendente = 1 or RealocacaoPendenteInterno = 1, 1, 0)) as QuantidadeTotal,
                                  (SELECT COUNT(*) FROM Indicado (NOLOCK)) as QuantidadeIndicados,
                                  (SELECT COUNT(*) FROM IndicadoInterno (NOLOCK) WHERE Inativo = 0) as QuantidadeIndicadosInterno
                            FROM CidadeParticipante (NOLOCK)";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                retorno = con.Query<CidadeRealocacaoPendente>(SQL).FirstOrDefault();

                con.Close();
                
            }
            if (retorno == null)
            {
                retorno = new CidadeRealocacaoPendente();
            }
            return retorno;

        }

        public CidadeParticipante CarregarPorCidade(int cidadeID, bool carregarQuantidades = false, bool condutor = false)
        {

            CidadeParticipante retorno;
            string SQL = @"SELECT TOP 1 cp.ID, 
								  cp.QuantidadeLugaresInterno, 
								  cp.QuantidadeLugares, 
                                  cp.Inativo,
                                  cp.RealocacaoPendente,  
                                  cp.RealocacaoPendenteInterno, " +
                                  ((carregarQuantidades) ? @"
                                      (SELECT COUNT(*) FROM Indicado (NOLOCK) WHERE CidadeParticipanteID = cp.ID " + (condutor ? " AND Condutor = 1" : "") + @") as QuantidadeIndicados,
                                      (SELECT COUNT(*) FROM IndicadoInterno (NOLOCK) WHERE CidadeParticipanteID = cp.ID AND Inativo = 0 " + (condutor ? " AND Condutor = 1" : "") + @") as QuantidadeIndicadosInterno,  "
                                    :"") + @"
                                  c.ID, c.Nome, 
								  e.ID, e.Nome, e.UF
                FROM CidadeParticipante (NOLOCK) cp
                INNER JOIN Cidade (NOLOCK) c ON cp.CidadeID = c.ID
                INNER JOIN Estado (NOLOCK) e ON c.EstadoID = e.ID
                WHERE cp.Inativo = 0 AND c.ID = @CidadeID";

            string SQLD = @"SELECT TOP 1 cp.ID, 
								  cp.QuantidadeLugaresInterno, 
								  cp.QuantidadeLugares, 
                                  cp.Inativo,
                                  cp.RealocacaoPendente,  
                                  cp.RealocacaoPendenteInterno, " +
                                  ((carregarQuantidades) ? @"
                                      (SELECT COUNT(*) FROM Indicado (NOLOCK) WHERE CidadeParticipanteID = cp.ID " + (condutor ? " AND Condutor = 1" : "") + @") as QuantidadeIndicados,
                                      (SELECT COUNT(*) FROM IndicadoInterno (NOLOCK) WHERE CidadeParticipanteID = cp.ID AND Inativo = 0 " + (condutor ? " AND Condutor = 1" : "") + @") as QuantidadeIndicadosInterno,  "
                                    : "") + @"
                                  c.ID, c.Nome, 
								  e.ID, e.Nome, e.UF,
                                  dbo.CalculaDistancia(c.Latitude, c.Longitude, cBuscando.Latitude, cBuscando.Longitude) as Distancia
                FROM CidadeParticipante (NOLOCK) cp
                INNER JOIN Cidade (NOLOCK) c ON cp.CidadeID = c.ID
                INNER JOIN Estado (NOLOCK) e ON c.EstadoID = e.ID
                INNER JOIN Cidade (NOLOCK) cBuscando ON cBuscando.ID = @CidadeID 
                WHERE cp.Inativo = 0 
                ORDER BY Distancia";
            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                try
                {
                    retorno = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL, (cidadeParticipante, cidade, estado) =>
                    {
                        cidadeParticipante.Cidade = cidade;
                        cidadeParticipante.Cidade.Estado = estado;
                        return cidadeParticipante;
                    }, new { CidadeID = cidadeID }).FirstOrDefault();

                    if (retorno == null)
                    {
                        retorno = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQLD, (cidadeParticipante, cidade, estado) =>
                        {
                            cidadeParticipante.Cidade = cidade;
                            cidadeParticipante.Cidade.Estado = estado;
                            return cidadeParticipante;
                        }, new { CidadeID = cidadeID }).FirstOrDefault();
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Error("SQL", SQL.Replace("@CidadeID", cidadeID.ToString()));
                    throw ex;
                }
                con.Close();

            }
            return retorno;
        }

        public bool CidadeParticipanteCadastrada(int id, int cidadeID)
        {
            string SQL = @"SELECT COUNT(1) QTDE
                           FROM CidadeParticipante
                           WHERE CidadeID = @CidadeID
                           AND ID <> @ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                return (con.Query<int>(SQL, new { ID = id, CidadeID = cidadeID }).FirstOrDefault() > 0);
            }
        }
        public IList<CidadeParticipante> ListarDecisaoPublica(Int32 skip, Int32 take, string palavraChave, int NotaMin)
        {
            IList<CidadeParticipante> result = new List<CidadeParticipante>();
            string SQLCount = @"SELECT COUNT(*) 
                                FROM CidadeParticipante (NOLOCK) cp
                                INNER JOIN Cidade (NOLOCK) c ON c.ID = cp.CidadeID
                                INNER JOIN Estado (NOLOCK) ON (c.EstadoID = Estado.ID)
                                WHERE (c.Nome like @palavraChave OR Estado.UF like @palavraChave) AND cp.Inativo = 0";

            string SQL = @"SELECT cp.ID
                        ,cp.QuantidadeLugares
                        ,cp.QuantidadeLugaresInterno
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) WHERE Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as SlotsOcupados
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) WHERE Indicado.Genero = 'F' AND Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as Mulheres
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) WHERE Indicado.Genero = 'M' AND Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as Homens
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) > 60 AND Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as Maior60
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) < 18 AND Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as Menor18
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 18 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 35 AND Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as Entre18_35
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 35 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 60 AND Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as Entre35_60
                        ,(SELECT COUNT (1) from Indicado (NOLOCK) 
                            INNER JOIN Historia (NOLOCK) ON Historia.ID = Indicado.HistoriaIDConcluido
                            WHERE Historia.Nota >= @NotaMin AND Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 0 ) as NaoCondutores
                        ,(SELECT AVG(Historia.Nota) FROM Historia (NOLOCK) INNER JOIN Indicado (NOLOCK) ON Indicado.HistoriaID = Historia.ID WHERE Indicado.CidadeParticipanteID = cp.ID AND Indicado.Condutor = 1) as NotaMedia
                        ,c.ID
                        ,c.Nome
                        ,Estado.ID
                        ,Estado.UF
                        FROM CidadeParticipante (NOLOCK) cp
                        INNER JOIN Cidade (NOLOCK) c ON (cp.CidadeID = c.ID)
                        INNER JOIN Estado (NOLOCK) ON (c.EstadoID = Estado.ID)
                        WHERE (Estado.UF +'-'+ c.Nome like @palavrachave) AND cp.Inativo = 0
	                    GROUP BY cp.ID, cp.QuantidadeLugares, cp.QuantidadeLugaresInterno, c.ID, c.Nome, Estado.ID, Estado.UF
                        ORDER BY Estado.UF ASC, c.Nome
                        OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                
                result = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL, (cidadePart, cidade, estado) =>
                {
                    cidadePart.Cidade = cidade;
                    cidadePart.Cidade.Estado = estado;
                    return cidadePart;
                },
                new
                {
                    palavraChave = "%" + palavraChave + "%",
                    skip = skip,
                    take = take,
                    Hoje = DateTime.Now,
                    NotaMin = NotaMin
                }).ToList();
                con.Close();
            }
            return result;
        }
        public IList<CidadeParticipante> ListarDecisaoInterna(Int32 skip, Int32 take, string palavraChave)
        {
            IList<CidadeParticipante> result = new List<CidadeParticipante>();
            string SQLCount = @"SELECT COUNT(*) 
                                FROM CidadeParticipante (NOLOCK) cp
                                INNER JOIN Cidade (NOLOCK) c ON c.ID = cp.CidadeID
                                INNER JOIN Estado (NOLOCK) ON (c.EstadoID = Estado.ID)
                                WHERE (c.Nome like @palavraChave OR Estado.UF like @palavraChave) AND cp.Inativo = 0";

            string SQL = @"SELECT cp.ID
                        ,cp.QuantidadeLugares
                        ,cp.QuantidadeLugaresInterno
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.Inativo = 0 AND Indicado.CidadeParticipanteID = cp.ID) as SlotsOcupados
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.Inativo = 0 AND Indicado.Genero = 'F' AND Indicado.CidadeParticipanteID = cp.ID) as Mulheres
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.Inativo = 0 AND Indicado.Genero = 'M' AND Indicado.CidadeParticipanteID = cp.ID) as Homens
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.Inativo = 0 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) > 60 AND Indicado.CidadeParticipanteID = cp.ID) as Maior60
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.Inativo = 0 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) < 18 AND Indicado.CidadeParticipanteID = cp.ID) as Menor18
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.Inativo = 0 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 18 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 35 AND Indicado.CidadeParticipanteID = cp.ID) as Entre18_35
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.Inativo = 0 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 35 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 60 AND Indicado.CidadeParticipanteID = cp.ID) as Entre35_60
                        ,c.ID
                        ,c.Nome
                        ,Estado.ID
                        ,Estado.UF
                        FROM CidadeParticipante (NOLOCK) cp
                        INNER JOIN Cidade (NOLOCK) c ON (cp.CidadeID = c.ID)
                        INNER JOIN Estado (NOLOCK) ON (c.EstadoID = Estado.ID)
                        WHERE (Estado.UF +'-'+ c.Nome like @palavrachave) AND cp.Inativo = 0 
	                    GROUP BY cp.ID, cp.QuantidadeLugares, cp.QuantidadeLugaresInterno, c.ID, c.Nome, Estado.ID, Estado.UF
                        ORDER BY Estado.UF ASC, c.Nome
                        OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();

                result = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL, (cidadePart, cidade, estado) =>
                {
                    cidadePart.Cidade = cidade;
                    cidadePart.Cidade.Estado = estado;
                    return cidadePart;
                },
                new
                {
                    palavraChave = "%" + palavraChave + "%",
                    skip = skip,
                    take = take,
                    Hoje = DateTime.Now
                }).ToList();
                con.Close();
            }
            return result;
        }
        public IList<CidadeParticipante> ListarNumerosConsolidados(Int32 skip, Int32 take, string palavraChave)
        {
            IList<CidadeParticipante> result = new List<CidadeParticipante>();
            string SQLCount = @"SELECT COUNT(*) 
                                FROM CidadeParticipante (NOLOCK) cp
                                INNER JOIN Cidade (NOLOCK) c ON c.ID = cp.CidadeID
                                INNER JOIN Estado (NOLOCK) ON (c.EstadoID = Estado.ID)
                                WHERE (c.Nome like @palavraChave OR Estado.UF like @palavraChave) AND cp.Inativo = 0";

            string SQL = @"SELECT cp.ID
                        ,cp.QuantidadeLugares
                        ,cp.QuantidadeLugaresInterno
                        ,(SELECT COUNT (1) FROM IndicadoInterno (NOLOCK) Interno WHERE Interno.Condutor = 1 AND Interno.CidadeParticipanteID = cp.ID) as SlotsOcupadosInterno
                        ,(SELECT COUNT (1) FROM Indicado (NOLOCK) Indicado WHERE Indicado.Condutor = 1 AND Indicado.CidadeParticipanteID = cp.ID) as SlotsOcupados
                        ,c.ID
                        ,c.Nome
                        ,Estado.ID
                        ,Estado.UF
                        FROM CidadeParticipante (NOLOCK) cp
                        INNER JOIN Cidade (NOLOCK) c ON (cp.CidadeID = c.ID)
                        INNER JOIN Estado (NOLOCK) ON (c.EstadoID = Estado.ID)
                        WHERE (Estado.UF +'-'+ c.Nome like @palavrachave) AND cp.Inativo = 0
	                    GROUP BY cp.ID, cp.QuantidadeLugares, cp.QuantidadeLugaresInterno, c.ID, c.Nome, Estado.ID, Estado.UF
                        ORDER BY Estado.UF ASC, c.Nome
                        OFFSET @skip ROWS FETCH NEXT @take ROWS ONLY ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();

                result = con.Query<CidadeParticipante, Cidade, Estado, CidadeParticipante>(SQL, (cidadePart, cidade, estado) =>
                {
                    cidadePart.Cidade = cidade;
                    cidadePart.Cidade.Estado = estado;
                    return cidadePart;
                },
                new
                {
                    palavraChave = "%" + palavraChave + "%",
                    skip = skip,
                    take = take
                }).ToList();
                con.Close();
            }
            return result;
        }

        public InfoSlotsDecisaoModel CarregarTotaisVisaoPublica()
        {
            InfoSlotsDecisaoModel retorno = new InfoSlotsDecisaoModel();
            
            string SQL = @"SELECT SUM (QuantidadeLugares) 
                        FROM CidadeParticipante (NOLOCK) cp
                        WHERE cp.Inativo = 0 ";

            string SQL2 = @"SELECT COUNT (1) 
                        FROM Indicado (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE cp.Inativo = 0 AND Indicado.Condutor = 1";

            string SQL3 = @"SELECT COUNT (1)
                        FROM Indicado (NOLOCK) 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) > 60 
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1";

            string SQL4 = @"SELECT COUNT (1)
                        FROM Indicado (NOLOCK) 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) < 18 
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1";

            string SQL5 = @"SELECT COUNT (1)
                        FROM Indicado (NOLOCK) 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 18 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 35
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1";

            string SQL6 = @"SELECT COUNT (1)
                        FROM Indicado (NOLOCK) 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 35 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 60
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1";

            string SQL7 = @"SELECT COUNT (1)
                        FROM Indicado (NOLOCK) 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE Indicado.Genero = 'F'
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1";

            string SQL8 = @"SELECT COUNT (1)
                        FROM Indicado (NOLOCK) 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE Indicado.Genero = 'M'
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1";
            string SQL9 = @"SELECT AVG(Historia.Nota) FROM Historia (NOLOCK) 
                            INNER JOIN Indicado (NOLOCK) ON Indicado.HistoriaID = Historia.ID
                            INNER JOIN CidadeParticipante (NOLOCK) cp ON cp.ID = Indicado.CidadeParticipanteID
                            WHERE Indicado.Condutor = 1 AND cp.Inativo = 0";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                retorno.SlotsTotais = con.ExecuteScalar<int>(SQL);
                retorno.SlotsOcupados = con.ExecuteScalar<int>(SQL2);
                retorno.Maior60 = con.ExecuteScalar<int>(SQL3, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Menor18 = con.ExecuteScalar<int>(SQL4, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Entre18_35 = con.ExecuteScalar<int>(SQL5, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Entre35_60 = con.ExecuteScalar<int>(SQL6, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Mulheres = con.ExecuteScalar<int>(SQL7);
                retorno.Homens = con.ExecuteScalar<int>(SQL8);
                retorno.Media = con.ExecuteScalar<int>(SQL9);
                con.Close();
            }

            return retorno;
        }
        public InfoSlotsDecisaoModel CarregarTotaisVisaoInterna()
        {
            InfoSlotsDecisaoModel retorno = new InfoSlotsDecisaoModel();

            string SQL = @"SELECT SUM (QuantidadeLugaresInterno) 
                        FROM CidadeParticipante (NOLOCK) cp
                        WHERE cp.Inativo = 0 ";

            string SQL2 = @"SELECT COUNT (1) 
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE cp.Inativo = 0 AND Indicado.Condutor = 1 AND Indicado.Inativo = 0";

            string SQL3 = @"SELECT COUNT (1)
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) > 60 
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1 AND Indicado.Inativo = 0";

            string SQL4 = @"SELECT COUNT (1)
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) < 18 
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1 AND Indicado.Inativo = 0";

            string SQL5 = @"SELECT COUNT (1)
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 18 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 35
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1 AND Indicado.Inativo = 0";

            string SQL6 = @"SELECT COUNT (1)
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) >= 35 AND (SELECT DATEDIFF(YEAR, Indicado.DataNascimento, @Hoje)) <= 60
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1 AND Indicado.Inativo = 0";

            string SQL7 = @"SELECT COUNT (1)
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE Indicado.Genero = 'F'
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1 AND Indicado.Inativo = 0";

            string SQL8 = @"SELECT COUNT (1)
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE Indicado.Genero = 'M'
                        AND cp.Inativo = 0 AND Indicado.Condutor = 1 AND Indicado.Inativo = 0";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                retorno.SlotsTotais = con.ExecuteScalar<int>(SQL);
                retorno.SlotsOcupados = con.ExecuteScalar<int>(SQL2);
                retorno.Maior60 = con.ExecuteScalar<int>(SQL3, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Menor18 = con.ExecuteScalar<int>(SQL4, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Entre18_35 = con.ExecuteScalar<int>(SQL5, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Entre35_60 = con.ExecuteScalar<int>(SQL6, new
                {
                    Hoje = DateTime.Today
                });
                retorno.Mulheres = con.ExecuteScalar<int>(SQL7);
                retorno.Homens = con.ExecuteScalar<int>(SQL8);
                con.Close();
            }

            return retorno;

        }
        public InfoSlotsDecisaoModel CarregarTotaisVisaoConsolidada()
        {
            InfoSlotsDecisaoModel retorno = new InfoSlotsDecisaoModel();

            string SQL = @"SELECT SUM (QuantidadeLugaresInterno) 
                        FROM CidadeParticipante (NOLOCK) cp
                        WHERE cp.Inativo = 0 ";

            string SQL2 = @"SELECT SUM (QuantidadeLugares) 
                        FROM CidadeParticipante (NOLOCK) cp
                        WHERE cp.Inativo = 0 ";

            string SQL3 = @"SELECT COUNT (1) 
                        FROM IndicadoInterno (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE cp.Inativo = 0 AND Indicado.Condutor = 1";

            string SQL4 = @"SELECT COUNT (1) 
                        FROM Indicado (NOLOCK) Indicado 
                        INNER JOIN CidadeParticipante (NOLOCK) cp ON (Indicado.CidadeParticipanteID = cp.ID)
                        WHERE cp.Inativo = 0 AND Indicado.Condutor = 1";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                retorno.SlotsTotais = con.ExecuteScalar<int>(SQL);
                retorno.SlotsTotais += con.ExecuteScalar<int>(SQL2);
                retorno.SlostsOcupadosInternos = con.ExecuteScalar<int>(SQL3);
                retorno.SlotsOcupados = con.ExecuteScalar<int>(SQL4);

                con.Close();
            }

            return retorno;
        }
    }
}
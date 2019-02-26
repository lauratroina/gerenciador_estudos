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

    public class ModeracaoRespostaADO : ADOSuper, IModeracaoRespostaDAL
    {
        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        /// <summary>
        /// Carregar a cidade pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModeracaoResposta Carregar(int id)
        {

            ModeracaoResposta entidade = null;
            string SQL = @"SELECT TOP 1 ID,
                                 Texto, Valor
                            FROM ModeracaoResposta 
                            WHERE ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<ModeracaoResposta>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de cidades a partir do id do estado
        /// </summary>
        /// <param name="estadoID"></param>
        /// <returns></returns>
        public IList<ModeracaoResposta> Listar(int moderacaoPerguntaID)
        {
            IList<ModeracaoResposta> list = new List<ModeracaoResposta>();

            string SQL = @"SELECT ID, Texto, Valor
                            FROM ModeracaoResposta
                            WHERE ModeracaoPerguntaID = @ModeracaoPerguntaID
                            AND Inativo = @Inativo
                            ORDER BY Ordem";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<ModeracaoResposta>(SQL, 
                    new 
                    { 
                        ModeracaoPerguntaID = moderacaoPerguntaID,
                        Inativo = false
                    }).ToList();

                con.Close();
            }

            return list;
        }

    }
}
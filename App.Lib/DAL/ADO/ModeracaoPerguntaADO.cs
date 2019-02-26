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

    public class ModeracaoPerguntaADO : ADOSuper, IModeracaoPerguntaDAL
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
        public ModeracaoPergunta Carregar(int id)
        {

            ModeracaoPergunta entidade = null;
            string SQL = @"SELECT TOP 1 ID, Texto, Tipo
                            FROM ModeracaoPergunta
                           WHERE ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<ModeracaoPergunta>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de cidades a partir do id do estado
        /// </summary>
        /// <param name="estadoID"></param>
        /// <returns></returns>
        public IList<ModeracaoPergunta> Listar()
        {
            IList<ModeracaoPergunta> list = new List<ModeracaoPergunta>();

            string SQL = @"SELECT ID, Texto, Tipo as TipoDB
                            FROM ModeracaoPergunta 
                            WHERE (Inativo = @Inativo 
                                OR
                                  Tipo = @Tipo)
                            ORDER BY Ordem";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<ModeracaoPergunta>(SQL, new { Inativo = false, Tipo = enumTipoPerguntaModeracao.categoria.ValueAsString() }).ToList();

                con.Close();
            }

            return list;
        }

    }
}
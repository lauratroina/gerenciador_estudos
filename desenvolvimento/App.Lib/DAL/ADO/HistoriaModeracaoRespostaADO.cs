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

    public class HistoriaModeracaoRespostaADO : ADOSuper, IHistoriaModeracaoRespostaDAL
    {
        /// <summary> 
        /// Inclui dados na base 
        /// </summary> 
        /// <param name="entidade"></param> 
        public void Inserir(HistoriaModeracaoResposta entidade)
        {
            string SQL = @" INSERT INTO HistoriaModeracaoResposta
                (Valor, HistoriaModeracaoID, ModeracaoPerguntaID, HistoriaCategoriaID, ModeracaoRespostaID) 
            VALUES (@Valor, @HistoriaModeracaoID, @ModeracaoPerguntaID, @HistoriaCategoriaID, @ModeracaoRespostaID)";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

		 /// <summary> 
        /// Método que retorna todas as entidades. 
        /// </summary> 
        public IList<HistoriaModeracaoResposta> Listar(int historiaModeracaoID) 
        {
            IList<HistoriaModeracaoResposta> list = null;
            string SQL = @"SELECT ID, HistoriaCategoriaID
                           FROM HistoriaCategoriaHistoria (NOLOCK)
                           WHERE HistoriaID = @HistoriaID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<HistoriaModeracaoResposta>(SQL, new { HistoriaID = historiaModeracaoID }).ToList();

                con.Close();

            }

            return list;
        }
		
    }
}
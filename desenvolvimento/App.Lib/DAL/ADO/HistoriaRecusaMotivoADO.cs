using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using BradescoNext.Lib.Entity;
using System.Linq;
using Dapper;

namespace BradescoNext.Lib.DAL.ADO
{
    public class HistoriaRecusaMotivoADO : ADOSuper, IHistoriaRecusaMotivoDAL
    {
        /// <summary>
        /// Método que retorna a lista de empresas
        /// </summary>
        /// <returns></returns>
        public void Incluir(int historiaID, string listaRecusaMotivoID)
        {

            string SQL = string.Format(@"INSERT INTO HistoriaRecusaMotivo (HistoriaID, RecusaMotivoID)
                                        (SELECT @HistoriaID, ID
                                         FROM RecusaMotivo
                                         WHERE ID IN ({0}))", listaRecusaMotivoID);

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, new { HistoriaID = historiaID });
                con.Close();
            }
        }

        public IList<HistoriaRecusaMotivo> Listar(int historiaID)
        {
            IList<HistoriaRecusaMotivo> list = null;

            string SQL = @"SELECT RecusaMotivoID 
                            FROM HistoriaRecusaMotivo
                            WHERE HistoriaID = @HistoriaID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<HistoriaRecusaMotivo>(SQL, new { historiaID = historiaID }).ToList();

                con.Close();
            }

            return list;
        }
    }
}

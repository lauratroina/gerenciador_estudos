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
    public class RecusaMotivoADO : ADOSuper, IRecusaMotivoDAL
    {
        /// <summary>
        /// Método que retorna a lista de empresas
        /// </summary>
        /// <returns></returns>
        public IList<RecusaMotivo> Listar()
        {
            IList<RecusaMotivo> list = null;

            string SQL = @"SELECT ID, Descricao 
                            FROM RecusaMotivo
                            WHERE Inativo = 0
                            ORDER BY Descricao";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<RecusaMotivo>(SQL).ToList();

                con.Close();
            }

            return list;
        }

    }
}

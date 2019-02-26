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


namespace BradescoNext.Lib.DAL.ADO
{

    public class IndicadoInternoCategoriaADO : ADOSuper, IIndicadoInternoCategoriaDAL
    {

        public IList<IndicadoInternoCategoria> Listar()
        {
            IList<IndicadoInternoCategoria> list = new List<IndicadoInternoCategoria>();
            string SQL = @"SELECT ID, Nome
                             FROM IndicadoInternoCategoria 
                            ORDER BY Nome ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<IndicadoInternoCategoria>(SQL).ToList();

                con.Close();

            }

            return list;
        }

        public IndicadoInternoCategoria Carregar(int id)
        {
            IndicadoInternoCategoria entidade = null;
            string SQL = @"SELECT TOP 1 ID, 
                                        Nome
                            FROM IndicadoInternoCategoria 
                           WHERE ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<IndicadoInternoCategoria>(SQL, new { ID = id }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }
    }
}

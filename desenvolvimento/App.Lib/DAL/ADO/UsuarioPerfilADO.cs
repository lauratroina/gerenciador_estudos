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
using App.Lib.DAL;
using App.Lib.Entity;
using App.Lib.Entity.Enumerator;
using Dapper;
using System.Linq;


namespace App.Lib.DAL.ADO
{

    public class UsuarioPerfilADO : ADOSuper, IUsuarioPerfilDAL
    {

        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public IList<UsuarioPerfil> Listar()
        {

            IList<UsuarioPerfil> list = new List<UsuarioPerfil>();

            string SQL = @"SELECT * FROM UsuarioPerfil(NOLOCK) ORDER BY Descricao ASC";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<UsuarioPerfil>(SQL).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }
        

    }
}
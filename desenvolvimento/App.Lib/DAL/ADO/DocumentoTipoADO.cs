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

    public class DocumentoTipoADO : ADOSuper, IDocumentoTipoDAL
    {

		private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

		 /// <summary> 
        /// Método que retorna todas as entidades. 
		/// SELECT * FROM DocumentoTipo ORDER BY ID DESC 
        /// </summary> 
		public IList<DocumentoTipo> Listar() 
        {
            IList<DocumentoTipo> list = null;

            string SQL = @"SELECT ID, Nome
                            FROM DocumentoTipo
                            WHERE Inativo = @Inativo
                            ORDER BY Ordem";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<DocumentoTipo>(SQL,
                    new { Inativo = false }
                    ).ToList();

                con.Close();

            }
            return list;
        }
		
    }
}
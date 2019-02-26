using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using App.Lib.Util;
using App.Lib.Enumerator;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace App.Lib.DAL.ADO
{
    public abstract class ADOSuper
    {
        protected Database _db;
        static ADOSuper()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
            //Usage:
            //SqlMapper.AddTypeHandler(typeof(Enum), (SqlMapper.ITypeHandler)Activator.CreateInstance(typeof(EnumStringTypeHandler<>).MakeGenericType(typeof(Enum))));
        }

        public ADOSuper()
        {
            _db = DatabaseFactory.CreateDatabase();
        }

        private string ChaveCriptografia
        {
            set { }
            get { return ConfiguracaoAppUtil.Get(enumConfiguracaoLib.chaveCriptografia); }
        }

        public string Criptografar(string valor)
        {

            SqlParameter p = new SqlParameter("@a", valor);
            return " ENCRYPTBYPASSPHRASE('" + ChaveCriptografia + "', '" + p.SqlValue + "') ";
        }

        public string Decriptografar(string nome)
        {
            return " CONVERT(VARCHAR(MAX), DECRYPTBYPASSPHRASE('" + ChaveCriptografia + "', " + nome + ")) ";
        }
    }

    public class EnumStringTypeHandler<T> : SqlMapper.TypeHandler<T>
    {
        public override T Parse(object value)
        {
            if (value == null || value is DBNull) { return default(T); }
            return (T)Enum.Parse(typeof(T), value.ToString());
        }

        public override void SetValue(IDbDataParameter parameter, T value)
        {
            parameter.DbType = DbType.String;
            parameter.Value = value.ToString();
        }
    }

    
}

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
using System.Linq;
using Dapper;


namespace App.Lib.DAL.ADO
{

    public class UsuarioADO : ADOSuper
    {

        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        public void Inserir(Usuario entidade)
        {
            string SQL = @" INSERT INTO Usuario (Nome, Login, Senha, Inativo, Email) 
                                 VALUES (@Nome, @Login, @Senha, @Inativo, @Email) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public void Atualizar(Usuario entidade)
        {
            string SQL = @"UPDATE Usuario SET  
                                Login = @Login, 
                                Senha = @Senha,
                                Nome = @Nome,
                                Inativo = @Inativo, 
                                Email = @Email
                          WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        public Usuario Carregar(int id)
        {
            Usuario entidade = null;
            string SQL = @"SELECT TOP 1 
                                u.ID, u.Nome, u.Login, u.Senha, u.Inativo, u.Email                             
                            FROM Usuario (NOLOCK) u
                           WHERE u.ID=@ID";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Usuario>(SQL, new
                {
                    ID = id
                }).FirstOrDefault();

                con.Close();
            }

            return entidade;
        }

        public Usuario Carregar(string login)
        {
            Usuario entidade = null;
            string SQL = @"SELECT TOP 1 
                                u.ID, u.Nome, u.Login, u.Senha, u.Inativo, u.Email
                                    FROM Usuario (NOLOCK) u
                           WHERE u.Login=@Login";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                entidade = con.Query<Usuario>(SQL, new
                {
                    Login=login
                }).FirstOrDefault();

                con.Close();
            }
            return entidade;
        }

         public IList<Usuario> Listar()
        {
            IList<Usuario> list = new List<Usuario>();

            string SQL = @"SELECT * FROM Usuario(NOLOCK) ORDER BY Nome";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                list = con.Query<Usuario>(SQL).ToList();

                con.Close();
            }
            TotalRegistros = list.Count;
            return list;
        }

     


    }
}
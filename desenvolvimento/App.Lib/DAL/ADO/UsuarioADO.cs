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

    public class UsuarioADO : ADOSuper, IUsuarioDAL
    {

        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        /// <summary>
        /// Método que insere um usuário na base de dados
        /// </summary>
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        /// <returns></returns>
        public void Inserir(Usuario entidade)
        {
            string SQL = @" INSERT INTO Usuario (Nome, Login, Senha, UsuarioPerfilID, Inativo, UsuarioID, DataCadastro, Email) 
                                 VALUES (@Nome, @Login, @Senha, @UsuarioPerfilID, @Inativo, @UsuarioID, @DataCadastro, @Email) ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }

        /// <summary>
        /// Método que atualiza os dados de um usuário na base de dados através de seu ID
        /// </summary>
        /// <param name="entidade">Entidade contendo os dados a serem atualizados.</param> 
        /// <returns></returns>
        public void Atualizar(Usuario entidade)
        {
            string SQL = @"UPDATE Usuario SET  
                                Login = @Login, 
                                Senha = @Senha,
                                Nome = @Nome,
                                UltimoAcesso = (CASE WHEN @UltimoAcesso > '1900-01-01' THEN @UltimoAcesso ELSE NULL END),
                                Inativo = @Inativo, 
                                Email = @Email,
                                UsuarioPerfilID = @UsuarioPerfilID
                          WHERE ID=@ID ";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();
                con.Execute(SQL, entidade);
                con.Close();
            }
        }
        /// <summary>
        /// Método que carrega os dados de um usuário através de seu id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Usuario Carregar(int id)
        {
            Usuario entidade = null;
            string SQL = @"SELECT TOP 1 
                                u.ID, u.Nome, u.Login, u.Senha, u.UltimoAcesso, u.Inativo, u.DataCadastro, u.Email, u.UsuarioPerfilID, u.UsuarioID,
                                p.ID, p.Nome as NomeDB, p.Descricao
                            FROM Usuario (NOLOCK) u
                      INNER JOIN UsuarioPerfil (NOLOCK) p ON u.UsuarioPerfilID = p.ID
                           WHERE u.ID=@ID";

            //using (DbConnection con = _db.CreateConnection())
            //{
            //    con.Open();

            //    entidade = con.Query<Usuario, UsuarioPerfil, Usuario>(SQL, (usuario, perfil) =>
            //    {
            //        usuario.Perfil = perfil;
            //        return usuario;
            //    }, new
            //    {
            //        ID = id
            //    }).FirstOrDefault();

            //    con.Close();
            //}
            entidade = new Usuario
            {
                Ativo = true,
                Email = "lombardi.rdn@gmail.com",
                ID = 1,
                Login = "rudi",
                Perfil = new UsuarioPerfil
                {
                    ID = 1,
                    Nome = Entity.Enumerator.enumPerfilNome.master,
                    Descricao = "Master"
                },
                Nome = "Rudinei Lombardi"
            };

            return entidade;
        }

        /// <summary>
        /// Método que carrega os dados de um usuário através de seu login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public Usuario Carregar(string login)
        {
            Usuario entidade = null;
            string SQL = @"SELECT TOP 1 
                                u.ID, u.Nome, u.Login, u.Senha, u.UltimoAcesso, u.Inativo, u.DataCadastro, u.Email, u.UsuarioPerfilID, u.UsuarioID,
                                p.ID, p.Nome as NomeDB, p.Descricao
                            FROM Usuario (NOLOCK) u
                      INNER JOIN UsuarioPerfil (NOLOCK) p ON u.UsuarioPerfilID = p.ID
                           WHERE u.Login=@Login";

            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                //entidade = con.Query<Usuario, UsuarioPerfil, Usuario>(SQL, (usuario, perfil) =>
                //{
                //    usuario.Perfil = perfil;
                //    return usuario;
                //}, new
                //{
                //    Login = login
                //}).FirstOrDefault();

                entidade = new Usuario
                {
                    Ativo = true,
                    Email = "lombardi.rdn@gmail.com",
                    ID = 1,
                    Login = "rudi",
                    Perfil = new UsuarioPerfil
                    {
                        ID = 1,
                        Nome = Entity.Enumerator.enumPerfilNome.master,
                        Descricao = "Master"
                    },
                    Nome = "Rudinei Lombardi"
                };

                con.Close();
            }
            return entidade;
        }

        /// <summary>
        /// Método que retorna a lista de usuarios
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Método que retorna a lista de usuarios de uma página
        /// </summary>
        /// <param name="paginaAtual"></param>
        /// <param name="totalRegPorPagina"></param>
        /// <returns></returns>
        public IList<Usuario> Listar(Int32 skip, Int32 take, string palavraChave, bool somenteAtivos = true)
        {

            IList<Usuario> list = new List<Usuario>();

            string SQLCount = @"SELECT COUNT(*) FROM Usuario(NOLOCK) WHERE nome LIKE @palavraChave  " + (somenteAtivos ? " AND Inativo = 0" : "");

            // SQL SERVER 2008
            string SQL = @"
                            SELECT tbl.ID, tbl.Nome, tbl.Login, tbl.Senha, tbl.UltimoAcesso, tbl.Inativo, tbl.DataCadastro, tbl.Email, tbl.UsuarioPerfilID, tbl.UsuarioID,
                                   tbl.perfilID as ID, tbl.perfilNomeDB as NomeDB, tbl.perfilDescricao as Descricao 
                              FROM (SELECT ROW_NUMBER() OVER(ORDER BY u.ID) AS NUMBER,
                                           u.ID, u.Nome, u.Login, u.Senha, u.UltimoAcesso, u.Inativo, u.DataCadastro, u.Email, u.UsuarioPerfilID, u.UsuarioID,
                                           p.ID as perfilID, p.Nome as perfilNomeDB, p.Descricao as perfilDescricao
                                      FROM Usuario (NOLOCK)  u
                                INNER JOIN UsuarioPerfil (NOLOCK) p ON u.UsuarioPerfilID = p.ID
                                WHERE u.nome like @palavraChave " + (somenteAtivos ? " AND Inativo = 0" : "") + @"
                            ) tbl
                            WHERE NUMBER BETWEEN @Skip AND (@Skip + @Take)
                            ORDER BY tbl.perfilNomeDB, tbl.Login";


            using (DbConnection con = _db.CreateConnection())
            {
                con.Open();

                TotalRegistros = con.Query<int>(SQLCount, new { palavraChave = "%" + palavraChave + "%" }).FirstOrDefault();
                list = con.Query<Usuario, UsuarioPerfil, Usuario>(SQL, (usuario, perfil) =>
                {
                    usuario.Perfil = perfil;
                    return usuario;
                }, new
                {
                    Skip = skip,
                    Take = take,
                    palavraChave = "%" + palavraChave + "%"
                }).ToList();
                con.Close();
            }

            return list;
        }


    }
}
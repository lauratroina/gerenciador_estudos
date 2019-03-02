using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using App.Lib.DAL;
using App.Lib.DAL.ADO;
using App.Lib.Entity;
using App.Lib.Entity.Enumerator;
using App.Lib.Enumerator;
using App.Lib.Identity;
using App.Lib.Models;
using System.Linq;

namespace App.Lib.Service
{
    public class UsuarioService
    {

        private Int32 _totalRegistros = 0;

        public Int32 TotalRegistros
        {
            get { return _totalRegistros; }
            set { _totalRegistros = value; }
        }

        private UsuarioADO dal = new UsuarioADO();

        public Usuario Carregar(int id)
        {
            return dal.Carregar(id);
        }

        public Usuario Carregar(string login)
        {
            return dal.Carregar(login);
        }

        public ClaimsIdentity CriarIdentity(Usuario usuario)
        {
            var userManager = new UserManager<UsuarioIdentity>(new UsuarioUserStore(this));
            var identity = userManager.CreateIdentity(usuario.CopyTo(new UsuarioIdentity()), DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }
        public async Task<ClaimsIdentity> CriarIdentityAsync(Usuario usuario)
        {
            var userManager = new UserManager<UsuarioIdentity>(new UsuarioUserStore(this));
            var identity = await userManager.CreateIdentityAsync(usuario.CopyTo(new UsuarioIdentity()), DefaultAuthenticationTypes.ApplicationCookie);
            return identity;
        }

        public void Salvar(Usuario usuario)
        {
            if (usuario.ID > 0)
            {
                dal.Atualizar(usuario);
            }
            else
            {
                dal.Inserir(usuario);
            }
        }

        public RetornoModel<Usuario, enumUsuarioException> Logar(string login, string senha)
        {
            Usuario usuario = Carregar(login);
            if (usuario == null)
            {
                return new RetornoModel<Usuario, enumUsuarioException>() { Sucesso = false, Mensagem = "Login ou Senha Inválidos", Retorno = usuario, Tipo = enumUsuarioException.usuarioNaoEncontrado };
            }
            else
            {
                if (VerificarSenha(usuario.Senha, senha))
                {
                    if (usuario.Inativo)
                    {
                        return new RetornoModel<Usuario, enumUsuarioException>() { Sucesso = false, Mensagem = "Usuário Inativo", Retorno = usuario, Tipo = enumUsuarioException.usuarioInativo };
                    }
                    else
                    {
                        return new RetornoModel<Usuario, enumUsuarioException>() { Sucesso = true, Retorno = usuario, Mensagem = "OK", Tipo = enumUsuarioException.nenhum };
                    }
                }
                else
                {
                    return new RetornoModel<Usuario, enumUsuarioException>() { Sucesso = false, Mensagem = "Login ou Senha Inválidos", Retorno = usuario, Tipo = enumUsuarioException.senhaInvalida };
                }
            }
        }

        public RetornoModel<Usuario, enumUsuarioException> MudarSenha(Usuario usuario, string senhaAnterior, string novaSenha)
        {
            if (VerificarSenha(usuario.Senha, senhaAnterior))
            {
                usuario.Senha = CriptografarSenha(novaSenha);
                Salvar(usuario);
                return new RetornoModel<Usuario, enumUsuarioException>() { Sucesso = true, Retorno = usuario, Mensagem = "OK", Tipo = enumUsuarioException.nenhum };
            }
            else
            {
                return new RetornoModel<Usuario, enumUsuarioException>() { Sucesso = false, Retorno = usuario, Mensagem = "A Senha atual não confere", Tipo = enumUsuarioException.senhaInvalida };
            }
        }

        public static string CriptografarSenha(string senha)
        {
            SHA256CryptoServiceProvider cryptoTransform = new SHA256CryptoServiceProvider();
            string hash = BitConverter.ToString(cryptoTransform.ComputeHash(Encoding.Default.GetBytes(senha))).Replace("-", "");
            return hash.ToLower();
        }

        public static bool VerificarSenha(string hash, string senha)
        {
            var senha2 = CriptografarSenha(senha);
            return (hash == CriptografarSenha(senha));
        }


        public static bool TemPermissao(Usuario usuario, params enumPerfilNome[] perfisNomes)
        {
            if (usuario == null)
                return false;
            if (perfisNomes == null || perfisNomes.Length == 0)
                return true;
            if (usuario.Perfil.Nome == enumPerfilNome.master)
                return true;
            return (perfisNomes.Contains(usuario.Perfil.Nome));
        }
    }
}

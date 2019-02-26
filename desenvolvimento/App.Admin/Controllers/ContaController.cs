using App.Admin.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity;
using App.Lib.Models;
using App.Lib.Entity;
using App.Lib.Service;
using App.Lib.Enumerator;
using Flurl;
using App.Admin.Enumerator;

namespace App.Admin.Controllers
{
    [OnlyHttps]
    public class ContaController : MasterController
    {
        public virtual ActionResult Index(string returnUrl = null)
        {
            if (returnUrl == null)
            {

                if (Request.UrlReferrer != null)
                {
                    returnUrl = Request.UrlReferrer.LocalPath;
                }
                else
                {
                    returnUrl = Url.Action("Index", "Home");
                }
                return RedirectToAction("Index", new { returnUrl = returnUrl });

            }

            ViewBag.ReturnUrl = returnUrl;
            if (SessionModel.Usuario != null)
            {
                return View("SemPermissao");
            }
            else
            {
                return View(new UsuarioViewModel());
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult Index(UsuarioViewModel usuario, string returnUrl = null)
        {
            UsuarioService service = new UsuarioService();
            if (string.IsNullOrEmpty(usuario.login) || string.IsNullOrEmpty(usuario.senha))
            {
                ModelState.AddModelError("", "É obrigatório o preenchimento de Login e Senha");
            }
            else
            {
                RetornoModel<Usuario, enumUsuarioException> retorno = service.Logar(usuario.login, usuario.senha);
                if (retorno.Sucesso)
                {
                    SessionModel.Usuario = retorno.Retorno;
                    SalvarSessao();

                    var AuthenticationManager = HttpContext.GetOwinContext().Authentication;
                    AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                    AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = true }, service.CriarIdentity(SessionModel.Usuario));

                    return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", retorno.Mensagem);
                }
            }
            ViewBag.ReturnUrl = returnUrl;
            return View(usuario);
        }

        public virtual ActionResult Sair()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public virtual ActionResult AlterarSenha(string returnUrl = null)
        {
            UsuarioService service = new UsuarioService();
            Usuario usuario = SessionModel.Usuario;

            if (returnUrl == null)
            {
                if (Request.UrlReferrer != null)
                {
                    returnUrl = Request.UrlReferrer.LocalPath;
                }
                else
                {
                    returnUrl = Url.Action("Index", "Home");
                }
            }
            ViewBag.ReturnUrl = returnUrl;

            UsuarioSenhaViewModel usuarioVM = new UsuarioSenhaViewModel();
            usuarioVM.id = usuario.ID;
            return View(usuarioVM);
        }

        [HttpPost]
        public virtual ActionResult AlterarSenha(UsuarioViewModel usuario, string returnUrl = null)
        {
            UsuarioService service = new UsuarioService();

            RetornoModel<Usuario, enumUsuarioException> retorno = service.MudarSenha(SessionModel.Usuario, usuario.senhaatual, usuario.senha);
            if(retorno.Sucesso)
            {
                returnUrl = returnUrl.SetQueryParams(new { msg = "Senha alterada com sucesso", msgtipo = enumTipoMensagem.sucesso });
                return Redirect(returnUrl);
            }
            else
            {
                ModelState.Clear();
                ModelState.AddModelError("", retorno.Mensagem);
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }

    }
}
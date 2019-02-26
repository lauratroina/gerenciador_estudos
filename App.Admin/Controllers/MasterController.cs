using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using App.Admin.Models;
using App.Admin.Enumerator;
using Microsoft.AspNet.Identity;
using System.Security.Principal;
using App.Lib.Service;
using App.Lib.Entity;
using App.Lib.Models;

namespace App.Admin.Controllers
{
    public class MasterController : Controller
    {

        protected SessionModel SessionModel = null;

        public static DateTime VariaveisUltimoCarregamento = DateTime.Now;
        public static bool VariaveisCarregadas = false;
        public static int VariaveisCarregamentoTTL = 20;

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CarregaVariaveis();
            VerificaMsgRetorno();
            base.OnActionExecuting(filterContext);
        }

        protected void CarregaVariaveis()
        {
            SessionModel = CarregaSessao(Session, User);
            if((!VariaveisCarregadas) || ((DateTime.Now - VariaveisUltimoCarregamento).Minutes > VariaveisCarregamentoTTL))
            {

                // AQUI VAO VARIAVEIS ESTATICAS QUE PRECISEM RECARREGAR

                VariaveisUltimoCarregamento = DateTime.Now;
                VariaveisCarregadas = true;
            }
        }

        public static SessionModel CarregaSessao(HttpSessionStateBase Session, IPrincipal User)
        {

            SessionModel SessionModel = Session.GetModel<SessionModel>();

            if (User.Identity.IsAuthenticated)
            {
                if (SessionModel.Usuario == null) 
                {
                    UsuarioService usuario = new UsuarioService();
                    SessionModel.Usuario = usuario.Carregar(Convert.ToInt32(IdentityExtensions.GetUserId(User.Identity)));
                    Session.SaveModel(SessionModel);
                }
            }
            else
            {
                if (SessionModel.Usuario != null)
                {
                    SessionModel.Usuario = null;
                    Session.SaveModel(SessionModel);
                }
            }
            return SessionModel;
        }

        protected void SalvarSessao()
        {
            Session.SaveModel(this.SessionModel);
        }

        protected void VerificaMsgRetorno()
        {
            ViewBag.msg = "";
            ViewBag.msgtipo = enumTipoMensagem.nula;

            if (Request.QueryString["msg"] != null)
            {
                ViewBag.msg = Request.QueryString["msg"].ToString();
                ViewBag.msgtipo = enumTipoMensagem.info;

                if (Request.QueryString["msgtipo"] != null)
                {
                    ViewBag.msgtipo = Enum.Parse(typeof(enumTipoMensagem), Request.QueryString["msgtipo"].ToString());
                }

            }
        }
    }
}
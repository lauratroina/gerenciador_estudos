using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Admin.Enumerator;
using App.Admin.Models;
using App.Lib.Entity.Enumerator;
using App.Lib.Service;

namespace App.Admin.Controllers
{
    [OnlyHttps]
    [AppAdminAuthorize]
    public class HomeController : MasterController
    {
        public ActionResult Index()
        {
            if(SessionModel.Usuario.Perfil.Nome == enumPerfilNome.convidado)
                return RedirectToAction("Filtrar", "FlashCard");
            return RedirectToAction("Index", "FlashCard");

        }
    }
}
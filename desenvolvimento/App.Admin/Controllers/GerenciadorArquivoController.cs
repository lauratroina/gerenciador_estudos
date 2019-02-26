using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Lib.Entity.Enumerator;

namespace App.Admin.Controllers
{
    [OnlyHttps]
    [AppAdminAuthorize(enumPerfilNome.master)]
    public class GerenciadorArquivoController : MasterController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
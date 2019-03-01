using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using App.Admin.Enumerator;
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

            return RedirectToAction("Index", "Materia");
        }
    }
}
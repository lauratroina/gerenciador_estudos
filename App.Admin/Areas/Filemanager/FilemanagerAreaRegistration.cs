using System.Web.Mvc;

namespace App.Admin.Areas.Filemanager
{
    public class FilemanagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Filemanager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Filemanager_default",
                "Filemanager/mvc",
                new { controller = "Filemanager", action = "Index" },
                new string[] { "App.Admin.Areas.FilemanagerArea.Controllers" }
            );
              
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using App.Admin.Controllers;
using App.Lib.Entity.Enumerator;
using App.Lib.Service;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AppAdminActionDesabilitada : AuthorizeAttribute 
{

    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        return false;
    }
}

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class AppAdminAuthorize : AuthorizeAttribute 
{
    private enumPerfilNome[] _perfisNomes = null;
    
    public AppAdminAuthorize() { }

    public AppAdminAuthorize(params enumPerfilNome[] perfisNomes)
    {
        _perfisNomes = perfisNomes;
    }


    protected override bool AuthorizeCore(HttpContextBase httpContext)
    {
        var SessionModel = MasterController.CarregaSessao(httpContext.Session, httpContext.User);
        return UsuarioService.TemPermissao(SessionModel.Usuario, _perfisNomes);
    }
}
using App.Lib.Util;
using App.Lib.Util.Enumerator;
/// <summary>
/// Forces a secured (HTTPS) request to be resent over HTTP
/// </summary>
using System;
using System.Web.Mvc;
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
public class OnlyHttpAttribute : FilterAttribute, IAuthorizationFilter
{
    public virtual void OnAuthorization(AuthorizationContext filterContext)
    {
        if (filterContext == null)
        {
            throw new ArgumentNullException("filterContext");
        }
        // Redireciona se urls que estiverem configurado
        var urlDominioReplace = ConfiguracaoAppUtil.GetAsDictionary(enumConfiguracaoGeral.urlDominioReplace);

        string port = "";
        if (filterContext.HttpContext.Request.Url.Port != 80)
        {
            port = ":" + filterContext.HttpContext.Request.Url.Port;
        }


        foreach (var url in urlDominioReplace)
        {
            if ((filterContext.HttpContext.Request.Url.Host.Contains(url.Key)) && (!filterContext.HttpContext.Request.Url.Host.Contains(url.Value)))
            {
                string urlWWW = "http://" + filterContext.HttpContext.Request.Url.Host.Replace(url.Key, url.Value) + port + filterContext.HttpContext.Request.RawUrl;
                filterContext.Result = new RedirectResult(urlWWW);
                return;
            }    
        } 
        // Only redirect GET requests
        if ((filterContext.HttpContext.Request.IsSecureConnection) && (string.Equals(filterContext.HttpContext.Request.HttpMethod, "GET", StringComparison.OrdinalIgnoreCase)))
        {
            string url = "http://" + filterContext.HttpContext.Request.Url.Host + filterContext.HttpContext.Request.RawUrl;
            filterContext.Result = new RedirectResult(url);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using App.Lib.Util.Enumerator;
using App.Lib.Util;

namespace System.Web.Mvc
{
    
    public static class UrlHelperExtensions
    {

        public static string ActionAbsoluteUri(this UrlHelper Url, string actionName, string controllerName, string protocol, object routeValues = null)
        {
            if((protocol=="https")&&(!ConfiguracaoAppUtil.GetAsBool(enumConfiguracaoGeral.httpsHabilitado)))
            {
                protocol = "http";
            }
            return Url.Action(actionName, controllerName, routeValues, protocol);
        }

        public static string ContentAbsoluteUri(this UrlHelper Url, string content)
        {
            string PathHost = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;

            if ((HttpContext.Current.Request.Url.Port != 80) && (HttpContext.Current.Request.Url.Port != 443))
            {
                PathHost += ":" + HttpContext.Current.Request.Url.Port.ToString();
            }

            if (content.StartsWith("~"))
            {
                content = PathHost + VirtualPathUtility.ToAbsolute(content);
            }
            return content;
        }

    }
}

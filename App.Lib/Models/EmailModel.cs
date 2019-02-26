using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using App.Lib.Enumerator;
using App.Lib.Util;
using App.Lib.Util.Enumerator;

namespace App.Lib.Models
{
    public class EmailModel<T>
    {
        public string PathImagens { get; set; }
        public string PathApp { get; set; }

        public string PathHost { get; set; }
        public T Model { get; set; }

    }

    public class EmailModel
    {
        public static EmailModel<T> GetModel<T>(T model)
        {
            EmailModel<T> emailModel = new EmailModel<T>();
            emailModel.Model = model;
            emailModel.PathImagens = ConfiguracaoAppUtil.Get(enumConfiguracaoLib.emailTemplateImagemUrl);
            if (!string.IsNullOrEmpty(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.hostUrlSite))){
                emailModel.PathApp = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.hostUrlSite);
                string contextoApp = emailModel.PathApp.RightOfIndexOf("://").RightOfIndexOf("/", true);
                if (string.IsNullOrEmpty(contextoApp))
                {
                    emailModel.PathHost = emailModel.PathApp;
                }
                else
                {
                    emailModel.PathHost = emailModel.PathApp.Replace(contextoApp, "");
                }
            }
            else if (HttpContext.Current != null)
            {
                emailModel.PathHost = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Host;
                if ((HttpContext.Current.Request.Url.Port != 80) && (HttpContext.Current.Request.Url.Port != 443))
                {
                    emailModel.PathHost += ":" + HttpContext.Current.Request.Url.Port.ToString();
                }
                emailModel.PathApp = emailModel.PathHost + VirtualPathUtility.ToAbsolute("~/");
            }
            else
            {
                emailModel.PathHost = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.hostUrlWithoutRequest);
                emailModel.PathApp = emailModel.PathHost + ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.appContextoWithoutRequest);
            }
            if (emailModel.PathImagens.StartsWith("~"))
            {
                if (HttpContext.Current != null)
                {   
                    emailModel.PathImagens = emailModel.PathHost + VirtualPathUtility.ToAbsolute(emailModel.PathImagens);
                }
                else
                {
                    
                    emailModel.PathImagens = emailModel.PathImagens.Replace("~", emailModel.PathApp);
                }
            }
            return emailModel;

        }


        
    }
}

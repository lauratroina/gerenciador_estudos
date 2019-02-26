using App.Lib.Util.Enumerator;
using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine;

namespace App.Lib.Util
{
    public static class RazorUtil
    {

        static RazorUtil()
        {
            var config = new TemplateServiceConfiguration();
            config.Namespaces.Add("RazorEngine.Text");
            var rns = ConfiguracaoAppUtil.GetAsList(enumConfiguracaoGeral.razorNamespaces);
            foreach(string ns in rns)
            {
                config.Namespaces.Add(ns);
            }

            Engine.Razor = RazorEngineService.Create(config);
        }

        public static string Render<T>(string path, T model)
        {
            FileInfo info = new FileInfo(path);
            string template = File.ReadAllText(path);
            return Engine.Razor.RunCompile(new LoadedTemplateSource(template, path), info.Name + info.LastWriteTime.ToUniversalTime(), typeof(T), model);
        }
        
    }
}
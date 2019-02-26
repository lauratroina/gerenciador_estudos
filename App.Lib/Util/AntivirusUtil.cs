using App.Lib.Util.Enumerator;
using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace App.Lib.Util
{
    public static class AntivirusUtil
    {



        public static bool Verificar(HttpPostedFileBase file)
        {

            string fileName = Path.GetFileName(file.FileName) + Guid.NewGuid().ToString();
            string diretorio = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string caminhoArquivo = Path.Combine(diretorio, fileName);

            file.SaveAs(caminhoArquivo);
            
            bool result = Verificar(caminhoArquivo);

            File.Delete(caminhoArquivo);

            return result;
        }
        public static bool Verificar(string caminhoArquivo)
        {
            bool result = false;
            if (string.IsNullOrEmpty(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.antivirusPrograma)))
            {
                result = true;
            }
            else
            {
                try
                {
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.FileName = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.antivirusPrograma);
                    startInfo.Arguments = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.antivirusParametros).Replace("{ARQUIVO}", "\"" + caminhoArquivo + "\"");
                    process.StartInfo = startInfo;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    result = output.Contains(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.antivirusVerificacao));
                }
                catch { result = true; }
            }
            
            return result;
        }
    }
}
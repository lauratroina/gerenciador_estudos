using App.Lib.Util.Enumerator;
using log4net;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace App.Lib.Util
{
    public static class EmailUtil
    {
        public static bool IsValidMailAdress(string email)
        {
            Regex rg = new Regex(@"^[a-zA-Z0-9.!#$%&amp;'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$");
            return rg.IsMatch(email);
        }
        public static void Send(string fromName, string from, string toName, string to, string subject, string message)
        {
            EmailUtil.Send(fromName + " <" + from + ">", toName + " <" + to + ">", subject, message);
        }

        public static void Send(string from, string to, string subject, string message)
        {
            MailMessage mail = new MailMessage(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailDefaultSender), to, subject, message);
            if(from != ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailDefaultSender)){
                mail.ReplyToList.Add(from);
            }
            
            mail.IsBodyHtml = true;
            EmailUtil.Send(mail);
        }
        public static void Send(string to, string subject, string message)
        {
            EmailUtil.Send(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailDefaultSender), to, subject, message);
        }

        public static void Send(MailMessage mail)
        {
            string securityProtocol = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailSecurityProtocol);
            if((!string.IsNullOrEmpty(securityProtocol)) && (securityProtocol != "default"))
            {
                ServicePointManager.SecurityProtocol = EnumExtensions.FromString<SecurityProtocolType>(securityProtocol);
            }

            SmtpClient smtp = new SmtpClient();
            smtp.Host = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailHost);
            smtp.Port = ConfiguracaoAppUtil.GetAsInt(enumConfiguracaoGeral.emailPort);
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            if (string.IsNullOrWhiteSpace(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailUser)) || string.IsNullOrWhiteSpace(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailPass)))
            {
                smtp.UseDefaultCredentials = true;
            }
            else
            {
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailUser), ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.emailPass));

            }
            smtp.EnableSsl = ConfiguracaoAppUtil.GetAsBool(enumConfiguracaoGeral.emailSSL);
            smtp.Send(mail);
        }
    }
}
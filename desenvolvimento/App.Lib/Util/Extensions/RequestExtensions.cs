using App.Lib.Util;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace System.Web
{
    public static class RequestExtensions
    {

        public static string GetUserIPAddress(this HttpRequestBase request)
        {
            string ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return request.ServerVariables["REMOTE_ADDR"];
        }

        public static IPrincipal GetUserByCookie(this HttpRequestBase request, string cookieName)
        {
            try
            {
                if (request.Cookies.AllKeys.Contains(cookieName))
                {
                    string ticket = request.Cookies[cookieName].Value;
                    ticket = ticket.Replace('-', '+').Replace('_', '/');

                    var padding = 3 - ((ticket.Length + 3) % 4);
                    if (padding != 0)
                        ticket = ticket + new string('=', padding);

                    var bytes = Convert.FromBase64String(ticket);

                    bytes = System.Web.Security.MachineKey.Unprotect(bytes,
                        "Microsoft.Owin.Security.Cookies.CookieAuthenticationMiddleware",
                            "ApplicationCookie", "v1");

                    using (var memory = new MemoryStream(bytes))
                    {
                        using (var compression = new GZipStream(memory,
                                                            CompressionMode.Decompress))
                        {
                            using (var reader = new BinaryReader(compression))
                            {
                                reader.ReadInt32();
                                string authenticationType = reader.ReadString();
                                reader.ReadString();
                                reader.ReadString();

                                int count = reader.ReadInt32();

                                var claims = new Claim[count];
                                for (int index = 0; index != count; ++index)
                                {
                                    string type = reader.ReadString();
                                    type = type == "\0" ? ClaimTypes.Name : type;

                                    string value = reader.ReadString();

                                    string valueType = reader.ReadString();
                                    valueType = valueType == "\0" ?
                                                   "http://www.w3.org/2001/XMLSchema#string" :
                                                     valueType;

                                    string issuer = reader.ReadString();
                                    issuer = issuer == "\0" ? "LOCAL AUTHORITY" : issuer;

                                    string originalIssuer = reader.ReadString();
                                    originalIssuer = originalIssuer == "\0" ?
                                                                 issuer : originalIssuer;

                                    claims[index] = new Claim(type, value,
                                                           valueType, issuer, originalIssuer);
                                }

                                var identity = new ClaimsIdentity(claims, authenticationType,
                                                              ClaimTypes.Name, ClaimTypes.Role);

                                var principal = new ClaimsPrincipal(identity);

                                return principal;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtil.Error(ex);
            }
            return null;
            
        }

        
        public static List<string> GetClientIPAddress(this HttpRequest request)
        {
            List<string> ipAddress;
            if (!string.IsNullOrEmpty(request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
            {
                ipAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',').Select(t => t.Trim()).ToList();
            }
            else
            {
                ipAddress = new List<string>();
            }
            if (!ipAddress.Contains(request.ServerVariables["REMOTE_ADDR"]))
            {
                ipAddress.Add(request.ServerVariables["REMOTE_ADDR"]);
            }
            return ipAddress;
        }

        public static string GetClientReferer(this HttpRequest request)
        {
            if (request.UrlReferrer != null)
            {
                string url = request.UrlReferrer.Scheme + "://" + request.UrlReferrer.Host;
                if (((request.UrlReferrer.Scheme == "http") && (request.UrlReferrer.Port != 80)) || ((request.UrlReferrer.Scheme == "https") && (request.UrlReferrer.Port != 443)))
                {
                    url += ":" + request.UrlReferrer.Port;
                }

            }
            return null;
        }

        public static CultureInfo GetBrowserCulture(this HttpRequestBase request)
        {
            string[] languages = request.UserLanguages;

            if (languages == null || languages.Length == 0)
                return null;

            try
            {
                string language = languages[0].ToLowerInvariant().Trim();
                return CultureInfo.CreateSpecificCulture(language);
            }
            catch (ArgumentException)
            {
                return null;
            }
        }

    }
}
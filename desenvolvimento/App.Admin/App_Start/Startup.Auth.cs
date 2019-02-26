using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using App.Lib.Util;
using App.Lib.Util.Enumerator;

namespace App.Admin
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.sessionModelKey),
                CookieDomain = ConfiguracaoAppUtil.Get(enumConfiguracaoGeral.cookieDominio),
                CookieHttpOnly = false,
                LoginPath = new PathString("/Conta")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            
        }
    }
}
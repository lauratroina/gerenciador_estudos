using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(App.Admin.Startup))]
namespace App.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

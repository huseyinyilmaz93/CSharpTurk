using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(AspNetMVC_CSharpTurk.App_Start.Startup))]

namespace AspNetMVC_CSharpTurk.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions 
            {
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Hesap/GirisYap")
            });
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}

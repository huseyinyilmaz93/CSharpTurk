using AspNetMVC_CSharpTurk.API.Etkinlik;
using AspNetMVC_CSharpTurk.API.Haber;
using AspNetMVC_CSharpTurk.API.Ilan;
using AspNetMVC_CSharpTurk.Models.MailModels;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace AspNetMVC_CSharpTurk
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //HaberAPI.KontrolEt();
            //EtkinlikAPI.KontrolEt();
            //IlanAPI.KontrolEt();
        }
    }
}

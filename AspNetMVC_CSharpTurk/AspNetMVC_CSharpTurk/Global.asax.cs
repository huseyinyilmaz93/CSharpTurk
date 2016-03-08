using AspNetMVC_CSharpTurk.API.Etkinlik;
using AspNetMVC_CSharpTurk.API.Haber;
using AspNetMVC_CSharpTurk.API.Ilan;
using AspNetMVC_CSharpTurk.Models.MailModels;
using Autofac;
using elFinder.Connector.Integration.Autofac;
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
        IContainer _container;
        protected void Application_Start()
        {
            // register IoC
            var builder = new ContainerBuilder();
            // add other registrations...
            // add elFinder connector registration
            builder.RegisterElFinderConnectorDefault();
            // create container
            _container = builder.Build();
            // need also to set container in elFinder module
            _container.SetAsElFinderDependencyResolver();

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //HaberAPI.KontrolEt();
            //EtkinlikAPI.KontrolEt();
            //IlanAPI.KontrolEt();

            //string dir = "~/Files/Uploads/yunus.ozen/ch10";
            //string parent = System.Web.Hosting.HostingEnvironment.MapPath(dir);
            
            //string name = Path.GetFileName(dir);
            //string fileName = Path.Combine(parent, name + ".zip");
            //ZipFile.CreateFromDirectory(parent, fileName, CompressionLevel.Fastest ,true);
            //MakaleEtiketGuncelle e = new MakaleEtiketGuncelle();
            //e.Calistir();
        }
    }
}

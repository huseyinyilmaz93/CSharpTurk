using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AspNetMVC_CSharpTurk
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("elfinder.connector");



            routes.MapRoute(
                name: "Anasayfa",
                url: "Anasayfa",
                defaults: new { controller = "Site", action = "Index"}
            );

            routes.MapRoute(
                name: "MakaleDetay",
                url: "MakaleDetay/{MakaleUrl}",
                defaults: new { controller = "Makale", action = "MakaleDetay", MakaleUrl = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "HaberDetay",
                url: "HaberDetay/{HaberUrl}",
                defaults: new { controller = "Haber", action = "HaberDetay", HaberUrl = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EtkinlikDetay",
                url: "EtkinlikDetay/{EtkinlikUrl}",
                defaults: new { controller = "Etkinlik", action = "EtkinlikDetay", EtkinlikUrl = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "IsIlaniDetay",
                url: "IlanDetay/{IlanUrl}",
                defaults: new { controller = "IsIlani", action = "IlanDetay", IlanUrl = UrlParameter.Optional }
            );


            //routes.MapRoute(
            //    name: "Makale2",
            //    url: "Makale/{action}/{id}",
            //    defaults: new { controller = "Makale", action = "Makaleler", id = UrlParameter.Optional }
            //);


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Site", action = "Index", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "YazarID",
                url: "{controller}/{action}/{yazarId}",
                defaults: new { controller = "Site", action = "Index", yazarId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "YazarKayitResim",
                url: "{controller}/{action}/{form}",
                defaults: new { controller = "Site", action = "Index", form = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Arama",
                url: "{controller}/{action}/{aramaFiltresi}",
                defaults: new { controller = "Site", action = "Index", aramaFiltresi = UrlParameter.Optional }
            );
        }
    }
}

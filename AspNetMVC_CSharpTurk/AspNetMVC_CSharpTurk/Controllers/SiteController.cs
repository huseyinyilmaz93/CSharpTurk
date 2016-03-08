using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AspNetMVC_CSharpTurk.Filters;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class SiteController : PublicController
    {
        // GET: /Home/
        public ActionResult Index()
        {
            //ViewBag.kullaniciId = User.Identity.GetUserId();
            return View();
        }
        public ActionResult _NotAvaible()
        {
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View();
        }
        public ActionResult Deneme()
        {
            //ViewBag.kullaniciId = User.Identity.GetUserId();
            return View();
        }
        public ActionResult Yazarlar()
        {
            return View();
        }
        public ActionResult StatikSayfa(int id)
        {
            ViewBag.id = id;
            return View();
        }
        public ActionResult YazarMakaleler(string Id)
        {
            ViewBag.yazarId = Id;
            return View();
        }
        public ActionResult AramaSonucu (string id)
        {
            ViewBag.aramaFiltresi = id;
            return View();
        }
        public ActionResult SifremiUnuttum()
        {
            return View();
        }

    }
}
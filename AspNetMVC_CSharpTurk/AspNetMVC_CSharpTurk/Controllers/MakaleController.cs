using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AspNetMVC_CSharpTurk.Filters;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class MakaleController : PublicController
    {
        //
        // GET: /Makale/
        public ActionResult Makaleler()
        {
            ViewBag.Admin = false;
            if (User.IsInRole("Admin"))
                ViewBag.Admin = true;
            return View();
        }

        [MakaleArama]
        public ActionResult MakaleDetay(string MakaleUrl)
        {
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.aramaFiltresi = RouteData.Values["aramaFiltresi"];
            ViewBag.id = MakaleUrl.Substring(MakaleUrl.LastIndexOf("-")+1,(MakaleUrl.Length-1)-MakaleUrl.LastIndexOf("-"));
            return View();
        }

        public ActionResult MakaleTip(int id)
        {
            ViewBag.tipId = id;
            return View();
        }

        //
        // GET: /Makale/Create
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult MakaleOlustur()
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View();
        }

        //
        // GET: /Makale/Edit/5
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult MakaleDuzenle(int id)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.id = id;
            return View();
        }

        //
        // GET: /Makale/Delete/5
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult MakaleSil(int id)
        {
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.id = id;
            string userId = User.Identity.GetUserId();
            ViewBag.userId = userId;
            return View();
        }
    }
}
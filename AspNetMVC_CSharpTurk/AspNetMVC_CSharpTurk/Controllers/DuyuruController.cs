using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class DuyuruController : PublicController
    {
        //
        // GET: /Duyuru/
        public ActionResult Duyurular()
        {
            ViewBag.Admin = false;
            ViewBag.Yazar = false;
            if (User.IsInRole("Admin"))
                ViewBag.Admin = true;
            else if (User.IsInRole("Yazar"))
                ViewBag.Yazar = true;

            return View();
        }

        //
        // GET: /Duyuru/Details/5
        public ActionResult DuyuruDetay(int id)
        {
            ViewBag.id = id;
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View();
        }

        //
        // GET: /Duyuru/Create
        [Authorize(Roles = "Admin,Yazar")]

        public ActionResult DuyuruOlustur()
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View();
        }

        //
        // GET: /Duyuru/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult DuyuruDuzenle(int id)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.id = id;
            return View();
        }

        //
        // GET: /Duyuru/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DuyuruSil(int id)
        {
            ViewBag.userId = User.Identity.GetUserId();
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.id = id;
            return View();
        }
    }
}
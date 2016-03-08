using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
namespace AspNetMVC_CSharpTurk.Controllers
{
    public class IpucuController : PublicController
    {
        //
        // GET: /Ipucu/
        public ActionResult Ipuclari()
        {
            return View();
        }

        //
        // GET: /Ipucu/Details/5
        public ActionResult IpucuDetay(int id)
        {
            ViewBag.id = id;
            return View();
        }

        //
        // GET: /Ipucu/Create
        [Authorize(Roles = "Yazar")]
        public ActionResult IpucuOlustur()
        {
            return View();
        }

        //
        // GET: /Ipucu/Edit/5
        [Authorize(Roles = "Yazar")]
        public ActionResult IpucuSil(int id)
        {
            ViewBag.id = id;
            string userId = User.Identity.GetUserId();
            ViewBag.userId = userId;
            return View();
        }

        //
        // GET: /Ipucu/Delete/5
        [Authorize(Roles = "Yazar")]
        public ActionResult IpucuDuzenle(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
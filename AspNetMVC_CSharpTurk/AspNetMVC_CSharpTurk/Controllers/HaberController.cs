using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class HaberController : Controller
    {
        //
        // GET: /Haber/
        public ActionResult Haberler()
        {
            return View();
        }
        public ActionResult HaberDetay(string HaberUrl)
        {
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.haberId = HaberUrl.Substring(HaberUrl.LastIndexOf("-") + 1, (HaberUrl.Length - 1) - HaberUrl.LastIndexOf("-"));

            return View();
        }
	}
}
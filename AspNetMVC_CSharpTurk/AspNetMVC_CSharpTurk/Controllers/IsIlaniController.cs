using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class IsIlaniController : Controller
    {
        //
        // GET: /IsIlani/
        public ActionResult Ilanlar()
        {
            return View();
        }

        public ActionResult IlanDetay(string IlanUrl)
        {
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.ilanId = IlanUrl.Substring(IlanUrl.LastIndexOf("-") + 1, (IlanUrl.Length - 1) - IlanUrl.LastIndexOf("-"));
            return View();
        }
	}
}
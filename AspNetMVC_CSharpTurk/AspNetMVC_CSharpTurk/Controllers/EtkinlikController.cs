using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class EtkinlikController : Controller
    {
        //
        // GET: /Etkinlik/
        public ActionResult Etkinlikler()
        {
            return View();
        }
        public ActionResult EtkinlikDetay(string EtkinlikUrl)
        {
            ViewBag.previousUrl = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.etkinlikId = EtkinlikUrl.Substring(EtkinlikUrl.LastIndexOf("-") + 1, (EtkinlikUrl.Length - 1) - EtkinlikUrl.LastIndexOf("-"));
            return View();
        }
	}
}
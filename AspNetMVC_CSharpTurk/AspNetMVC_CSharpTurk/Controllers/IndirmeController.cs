using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class IndirmeController : PublicController
    {
        //
        // GET: /Indirme/
        public ActionResult Indirmeler()
        {
            return View();
        }

        //
        // GET: /Indirme/Details/5
        public ActionResult IndirmeDetay(int id)
        {
            ViewBag.id = id;
            return View();
        }

        //
        // GET: /Indirme/Create
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult IndirmeOlustur()
        {
            return View();
        }
        [HttpPost]
        public ActionResult IndirmeYukle(HttpPostedFileBase file,IndirmePostModel model)
        {
            if (file != null)
            {
                string dosyaAdi = Guid.NewGuid().ToString();
                string uzanti = System.IO.Path.GetExtension(file.FileName);
                string tamUzanti = uzanti.Substring(1);
                string tamAd = string.Concat(dosyaAdi, uzanti);
                string dosyaYolu = System.IO.Path.Combine(Server.MapPath("~/Files/Indirmeler"), tamAd);
                // file is uploaded
                file.SaveAs(dosyaYolu);
                ViewBag.dosya = tamUzanti + "-" + dosyaAdi;
                ViewBag.yazarId = User.Identity.GetUserId();
            }
            else
            {
                //ViewBag.dosya == "null";
            }
            return View();

        }

        //
        // GET: /Indirme/Edit/5
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult IndirmeDuzenle(int id)
        {
            ViewBag.id = id;
            return View();
        }

        //
        // GET: /Indirme/Delete/5
        [Authorize(Roles = "Admin,Yazar")]
        public ActionResult IndirmeSil(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
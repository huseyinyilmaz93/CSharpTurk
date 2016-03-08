using AspNetMVC_CSharpTurk.API.Etkinlik.Model;
using AspNetMVC_CSharpTurk.Models.AccountModels;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace AspNetMVC_CSharpTurk.Controllers
{
    [Authorize(Roles = "Admin")] 
    public class AdminController : PublicController
    {
        public ActionResult Index()
        {
            return View();
        }
        public AdminController()
        {
        }
        public ActionResult Kayit()
        {
            ViewBag.resim = "null";
            return View();
        }
        [HttpPost]
        public ActionResult Kayit(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string dosyaAdi = Guid.NewGuid().ToString();
                string uzanti = System.IO.Path.GetExtension(file.FileName);
                string tamUzanti = uzanti.Substring(1);
                string tamAd = string.Concat(dosyaAdi, uzanti);
                string dosyaYolu = System.IO.Path.Combine(Server.MapPath("~/Files/KullaniciResimleri"), tamAd);
                // file is uploaded
                file.SaveAs(dosyaYolu);
                ViewBag.resim = tamUzanti + "-" + dosyaAdi;
                ViewBag.resimYolu = "/Files/KullaniciResimleri/" + tamAd;
            }
            return View();
        }
        public ActionResult DuyuruOnaylama()
        {
            return View();
        }
        public ActionResult YorumOnaylama()
        {
            return View();
        }
        public ActionResult HataOnaylama()
        {
            return View();
        }
        public ActionResult Yazarlar()
        {
            return View();

        }
        public ActionResult YazarDuzenle(string id)
        {
            ViewBag.yazarId = id;
            return View();
        }
        [HttpPost]
        public ActionResult DuzenlemeyiTamamla(string yazarId ,HttpPostedFileBase file ,YazarDuzenleModel model)
        {

                ViewBag.yazarId = yazarId;
                ViewBag.yazarId = yazarId;
                ViewBag.Ad = model.Name;
                ViewBag.Soyad = model.Surname;
                ViewBag.WebSite = model.WebSite;
                ViewBag.Email = model.EMail;
                if (file != null)
                {
                    string dosyaAdi = Guid.NewGuid().ToString();
                    string uzanti = System.IO.Path.GetExtension(file.FileName);
                    string tamUzanti = uzanti.Substring(1);
                    string tamAd = string.Concat(dosyaAdi, uzanti);
                    string dosyaYolu = System.IO.Path.Combine(Server.MapPath("~/Files/KullaniciResimleri"), tamAd);
                    // file is uploaded
                    file.SaveAs(dosyaYolu);
                    ViewBag.resim = tamUzanti + "-" + dosyaAdi;
                    ViewBag.resimYolu = "/Files/KullaniciResimleri/" + tamAd;
                }
                else
                {
                    ViewBag.resim = "null";
                }
                
            return View(model);
        }
        public ActionResult StatikSayfalar(string SayfaAdi)
        {
            return View();
        }
        public ActionResult StatikSayfaOlustur()
        {
            ViewBag.oncekiSayfa = System.Web.HttpContext.Current.Request.UrlReferrer;
            return View();
        }
        public ActionResult StatikSayfaDuzenle(int id)
        {
            ViewBag.oncekiSayfa = System.Web.HttpContext.Current.Request.UrlReferrer;
            ViewBag.id = id;
            return View();
        }


    }
}

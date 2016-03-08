using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AspNetMVC_CSharpTurk.Models.AccountModels;
using System.Net;
using System.Text;
using System.IO;
namespace AspNetMVC_CSharpTurk.Controllers
{
    [Authorize(Roles = "Yazar,Admin")]
    public class YazarController : PublicController
    {
        public ActionResult Index()
        {
            return View();
        }
        //[Authorize(Roles="Yazar")]
        // GET: /Yazar/
        public ActionResult Yazarlar()
        {
            return View();
        }
        public ActionResult Makaleler()
        {
            ViewBag.yazarId = User.Identity.GetUserId();
            return View();
        }
        public ActionResult Duyurular()
        {
            ViewBag.yazarId = User.Identity.GetUserId();
            return View();
        }
        public ActionResult Duzenle()
        {
            ViewBag.yazarId = User.Identity.GetUserId();
            return View();
        }
        public ActionResult ResimDegistir()
        {
            ViewBag.yazarId = User.Identity.GetUserId();
            return View();
        }
        [HttpPost]
        public ActionResult ResimDegistir(HttpPostedFileBase file)
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
                ViewBag.yazarId = User.Identity.GetUserId();
                ViewBag.resimYolu = "/Files/KullaniciResimleri/" + tamAd;
            }
            return View();
        }
        public ActionResult DosyaYukle()
        {
            //Configuration configuration = WebConfigurationManager.OpenWebConfiguration("~");
            //CustomErrorsSection section = (Custom) configuration.GetSection("elfinder");
            return View();
        }
        public ActionResult elFinder()
        {
            return View();
        }
        public ActionResult Deneme()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DuzenlemeyiTamamla(HttpPostedFileBase file, YazarDuzenleModel model)
        {
            ViewBag.yazarId = User.Identity.GetUserId();
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
        public ActionResult DosyaYoneticisi()
        {
            //var request = (HttpWebRequest)WebRequest.Create("http://localhost:65376/api/EtiketAPI/EtiketEkle/1?deneme=7?deneme=1?deneme=2");

            //var postData = "[{\"EtiketId\":1,\"EtiketAdi\":\"Microsoft\",\"EtiketRadi\":\"microsoft\",\"Makaleler\":null},{\"EtiketId\":2,\"EtiketAdi\":\".net\",\"EtiketRadi\":\"-net\",\"Makaleler\":null}]";
            //var data = Encoding.ASCII.GetBytes(postData);

            //request.Method = "POST";
            //request.ContentType = "application/x-www-form-urlencoded";
            //request.ContentLength = data.Length;

            //using (var stream = request.GetRequestStream())
            //{
            //    stream.Write(data, 0, data.Length);
            //}

            //var response = (HttpWebResponse)request.GetResponse();

            //var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

            ViewBag.userName = this.User.Identity.Name;
            return View();
        }
    }
}
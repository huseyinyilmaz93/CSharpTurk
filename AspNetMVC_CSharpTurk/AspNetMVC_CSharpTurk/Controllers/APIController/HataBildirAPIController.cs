using AspNetMVC_CSharpTurk.DAL;
using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    public class HataBildirAPIController : ApiController, IDisposable
    {
        DatabaseContextClass db = new DatabaseContextClass(); 

        [HttpGet]
        [Route("api/HataBildirAPI/KontrolEdilecek")]
        public IEnumerable<HataBildir> Hatalar()
        {
            return db.Hatalar.Where(h => h.Kontrol == false).ToList();
        } 
        [HttpPost]
        [Route("api/HataBildirAPI/HataKaydet")]
        public HttpResponseMessage HataKaydet (HataBildirPostModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("HataliGiris", "Tüm alanlar boş girilemez!");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
            try
            {
                Kategori catHata = db.Kategoriler.Where(k => k.KategoriAdi == "Hata").Select(k => k).FirstOrDefault();
                HataBildir hata = new HataBildir()
                {
                    Email = model.Email,
                    HataMesaji = model.HataMesaji,
                    BildirilmeTarihi = DateTime.Now,
                    Kontrol = false,
                    Onay = false,
                    Kategori = catHata,
                };
                db.Hatalar.Add(hata);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Veritabanı", "Veritabanına kaydedilirken bir hata ile karşılaşıldı. Tekrar deneyin!");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);
            }
        }
        [HttpHead]
        [Route("api/HataBildirAPI/HataOnay/{hataId}")]
        public HttpResponseMessage HataOnayla(int hataId)
        {
            if (db.Hatalar.Where(h => h.HataId == hataId) == null)
            {
                ModelState.AddModelError("Bulunamadı", "Verilen id değerine sahip bir hata bulunamadı");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
            try
            {
                db.Hatalar.Where(h => h.HataId == hataId).First().Kontrol = true;
                db.Hatalar.Where(h => h.HataId == hataId).First().Onay = true;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Veritabanı", "Veritabanına kaydedilirken bir hata ile karşılaşıldı.");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
        }
        [HttpHead]
        [Route("api/HataBildirAPI/HataRed/{hataId}")]
        public HttpResponseMessage HataReddet(int hataId)
        {
            if (db.Hatalar.Where(h => h.HataId == hataId) == null)
            {
                ModelState.AddModelError("Bulunamadı", "Verilen id değerine sahip bir hata bulunamadı");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
            try
            {
                db.Hatalar.Where(h => h.HataId == hataId).First().Kontrol = true;
                db.Hatalar.Where(h => h.HataId == hataId).First().Onay = false;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Veritabanı", "Veritabanına kaydedilirken bir hata ile karşılaşıldı.");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
        }
        [HttpDelete]
        [Route("api/HataBildirAPI/HataSil/{hataId}")]
        public HttpResponseMessage HataSil(int hataId)
        {
            if (db.Hatalar.Where(h => h.HataId == hataId) == null)
            {
                ModelState.AddModelError("Bulunamadı", "Verilen id değerine sahip bir hata bulunamadı");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
            try
            {
                HataBildir hata = db.Hatalar.Where(h => h.HataId == hataId).First();
                db.Hatalar.Remove(hata);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Veritabanı", "Veritabanına kaydedilirken bir hata ile karşılaşıldı.");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }

        }
        [HttpGet]
        [Route("api/HataBildirAPI/Onaylanacak")]
        public int Onaylanacak()
        {
            return db.Hatalar.Where(y => y.Kontrol == false).Select(y => y).ToList().Count;

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }
}

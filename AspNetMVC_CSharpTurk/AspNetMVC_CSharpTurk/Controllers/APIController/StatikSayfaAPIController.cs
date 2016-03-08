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
    [RoutePrefix("api/StatikSayfaAPI")]
    public class StatikSayfaAPIController : ApiController
    {
        DatabaseContextClass db = new DatabaseContextClass();

        [HttpGet,Route("StatikSayfalar")]
        public IEnumerable<StatikSayfa> StatikSayfalar()
        {
            return db.StatikSayfalar.ToList();
        }

        [HttpGet, Route("GetStatikSayfa/{statikSayfaId}")]
        public StatikSayfa GetStatikSayfa(int statikSayfaId)
        {
            return (db.StatikSayfalar.Include("Kategori").Where(s => s.SayfaId == statikSayfaId)).FirstOrDefault();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost,Route("StatikSayfaOlustur")]
        public HttpResponseMessage StatikSayfaOlustur(StatikSayfaPostModel model)
        {
            if(!ModelState.IsValid || model == null )
            {
                if (model == null) ModelState.AddModelError("Form", "Form elemanları boş bırakılamaz");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                StatikSayfa sayfa = new StatikSayfa()
                {
                    SayfaAciklama = model.SayfaAciklama,
                    SayfaIcerik = model.SayfaIcerik,
                    SayfaAdi = model.SayfaAdi,
                    GlyphiconClass = model.GlyphiconClass
                };
                sayfa.Kategori = db.Kategoriler.Where(k => k.KategoriAdi == "Statik Sayfa").FirstOrDefault();
                db.StatikSayfalar.Add(sayfa);
                db.SaveChanges();
                sayfa.SayfaRadi = sayfa.SayfaAdi.Replace(" ", "-").ToLower().Replace("ö", "o").Replace("ı","i").
                        Replace("ğ", "g").Replace("ç", "c").Replace("ü", "u").Replace("ş", "s") + "-" + sayfa.SayfaId;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Veritabanına yazarken hata oluştu. Tekrar deneyin.");
            }

            return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, "Bilinmeyen Hata");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut, Route("StatikSayfaDuzenle/{statikSayfaId}")]
        public HttpResponseMessage StatikSayfaDuzenle(int statikSayfaId,StatikSayfaPostModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("Form", "Form elemanları boş bırakılamaz");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if(db.StatikSayfalar.Where(s=>s.SayfaId == statikSayfaId) == null)
            {
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, "Tanımlanamayan Id değeri girildi!");
            }

            try
            {
                StatikSayfa sayfa = new StatikSayfa()
                {
                    SayfaId = statikSayfaId,
                    SayfaAciklama = model.SayfaAciklama,
                    SayfaIcerik = model.SayfaIcerik,
                    SayfaAdi = model.SayfaAdi,
                    GlyphiconClass = model.GlyphiconClass
                };

                sayfa.SayfaRadi = sayfa.SayfaAdi.Replace(" ", "-").ToLower().Replace("ö", "o").
                    Replace("ğ", "g").Replace("ç", "c").Replace("ü", "u").Replace("ş", "s") + "-" + sayfa.SayfaId;
                sayfa.Kategori = db.Kategoriler.Where(k => k.KategoriAdi == "Statik Sayfa").Select(k => k).FirstOrDefault();

                db.Entry(sayfa).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Veritabanına yazarken hata oluştu. Tekrar deneyin.");
            }

            return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, "Bilinmeyen Hata");

        }

        [Authorize(Roles = "Admin")]
        [HttpDelete, Route("StatikSayfaSil/{statikSayfaId}")]
        public HttpResponseMessage StatikSayfaSil(int statikSayfaId)
        {
            if (db.StatikSayfalar.Where(s => s.SayfaId == statikSayfaId) == null)
            {
                return Request.CreateResponse(HttpStatusCode.MethodNotAllowed, "Tanımlanamayan Id değeri girildi!");
            }
            try
            {
                db.StatikSayfalar.Remove(db.StatikSayfalar.Find(statikSayfaId));
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Veritabanına yazarken hata oluştu. Tekrar deneyin.");
            }
            return Request.CreateErrorResponse(HttpStatusCode.ServiceUnavailable, "Bilinmeyen Hata");
        }

    }
}

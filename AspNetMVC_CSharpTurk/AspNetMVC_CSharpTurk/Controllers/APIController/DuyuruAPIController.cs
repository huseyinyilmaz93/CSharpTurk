using AspNetMVC_CSharpTurk.DAL;
using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using AspNetMVC_CSharpTurk.Models.MembershipModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    public class DuyuruAPIController : ApiController
    {
        private DatabaseContextClass db = new DatabaseContextClass();
        // GET api/Duyuru [LIST Duyuru]
        [Route("api/DuyuruAPI")]
        [HttpGet]
        public IEnumerable<Duyuru> GetDuyurular()
        {
            return db.Duyurular.Where(d => d.Onay == true).Select(d => d).ToList();
        }

        // GET api/Duyuru/5 [GET Duyuru]
        //[ResponseType(typeof(Duyuru))]
        [HttpGet]
        [Route("api/DuyuruAPI/{id}")]
        public Duyuru GetDuyuru(int id)
        {

            var duyuru = db.Duyurular.Where(d => d.DuyuruId == id).Select(d => d);
            if (duyuru.Count() == 0)
            {
                return null;
            }
            return duyuru.FirstOrDefault();
        }

        // PUT api/Duyuru/5 [EDIT Duyuru]
        [HttpPut]
        [Route("api/DuyuruAPI/{id}/{userId}")]
        public HttpResponseMessage PutDuyuru(int id, string userId, DuyuruPostModel duyuru)
        {

            if (!ModelState.IsValid || duyuru == null)
            {
                if (duyuru == null) ModelState.AddModelError("formDuyuruDuzenle", "Tüm alanlar boş bırakılamaz !");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            Duyuru gecerliDuyuru = new Duyuru();

            Duyuru DuyuruKontrol = db.Duyurular.Where(d => d.DuyuruId == id && d.Yazar.Id == userId).Select(m => m).FirstOrDefault();

            if (this.GetRole(userId) == null) return Request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError());
            if (DuyuruKontrol == null) return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, new HttpError());

            bool hasChanged = (DuyuruKontrol.DuyuruBaslik != duyuru.DuyuruBaslik ||
            DuyuruKontrol.DuyuruGovde != duyuru.DuyuruGovde ||
            DuyuruKontrol.DuyuruOzet != duyuru.DuyuruOzet ? true : false);

            if (hasChanged)
            {
                db.Duyurular.Where(m => m.DuyuruId == id).Select(m => m).First().DuyuruBaslik = duyuru.DuyuruBaslik;
                db.Duyurular.Where(m => m.DuyuruId == id).Select(m => m).First().DuyuruGovde = duyuru.DuyuruGovde;
                db.Duyurular.Where(m => m.DuyuruId == id).Select(m => m).First().DuyuruOzet = duyuru.DuyuruOzet;
            }
            else return Request.CreateResponse(HttpStatusCode.NoContent);

            try
            {
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DuyuruExists(id))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
        }

        // POST api/Duyuru [CREATE Duyuru]
        [HttpPost]
        [Route("api/DuyuruAPI/{userId}")]
        [ResponseType(typeof(DuyuruPostModel))]
        public HttpResponseMessage PostDuyuru(DuyuruPostModel model, string userId)
        {
            Duyuru duyuru = new Duyuru();
            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("formMakaleOlustur", "Tüm alanlar boş bırakılamaz !");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            try
            {
                duyuru.DuyuruBaslik = model.DuyuruBaslik;
                duyuru.DuyuruGovde = model.DuyuruGovde;
                duyuru.DuyuruOzet = model.DuyuruOzet;
                duyuru.DuyuruResimURL = "--UNKNOWN--";
                duyuru.DuyuruTarih = DateTime.Now;
                duyuru.Onay = false;
                duyuru.Kontrol = false;
                if (User.IsInRole("Admin"))
                {
                    duyuru.Onay = true;
                    duyuru.Kontrol = true;
                }
                var kategori = db.Kategoriler.Where(k => k.KategoriAdi == "Duyuru").Select(k => k).FirstOrDefault();
                var yazar = db.Users.Where(u => u.Id == userId).Select(u => u).FirstOrDefault();
                duyuru.Kategori = kategori;
                duyuru.Yazar = yazar;

                db.Duyurular.Add(duyuru);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway, new HttpError());
            }
        }

        // DELETE api/Duyuru/5 [DELETE Duyuru]
        [ResponseType(typeof(Duyuru))]
        [HttpDelete]
        [Route("api/DuyuruAPI/{id}/{userId}")]
        public IHttpActionResult DeleteDuyuru(int id, string userId)
        {

            Duyuru duyuru = db.Duyurular.Find(id);

            //Duyurunun sahibi yada admin duyuruyu değiştirebilir.
            Duyuru duyuruKontrol = db.Duyurular.Where(d => d.DuyuruId == id && d.Yazar.Id == userId).Select(m => m).FirstOrDefault();
            //Yazar yazarKontrol = db.Yazarlar.Where(y => y.Id == userId && y.UserName == "admin").Select(y => y).FirstOrDefault();

            if (duyuruKontrol == null && GetRole(userId) != "Admin")
            {
                return StatusCode(HttpStatusCode.Ambiguous);
            }


            if (duyuru == null)
            {
                return NotFound();
            }

            db.Duyurular.Remove(duyuru);
            db.SaveChanges();

            return Ok(duyuru);
        }

        [HttpGet]
        [Route("api/DuyuruAPI/Kontrol")]
        public IEnumerable<Duyuru> GetDuyuruKontrol()
        {
            return db.Duyurular.Where(d => d.Kontrol == false).Select(d => d).ToList();
        }

        [HttpGet]
        [Route("api/DuyuruAPI/Onaylanacak")]
        public int Onaylanacak()
        {
            return db.Duyurular.Where(y => y.Kontrol == false).Select(y => y).ToList().Count;

        }
        [HttpHead]
        [Route("api/DuyuruAPI/Onay/{id}")]
        public IHttpActionResult DuyuruOnayla(int id)
        {
            db.Duyurular.Where(d => d.DuyuruId == id).Select(d => d).FirstOrDefault().Onay = true;
            db.Duyurular.Where(d => d.DuyuruId == id).Select(d => d).FirstOrDefault().Kontrol = true;
            db.SaveChanges();
            return Ok();
        }

        [HttpHead]
        [Route("api/DuyuruAPI/Red/{id}")]
        public IHttpActionResult DuyuruReddet(int id)
        {
            db.Duyurular.Where(d => d.DuyuruId == id).Select(d => d).FirstOrDefault().Onay = false;
            db.Duyurular.Where(d => d.DuyuruId == id).Select(d => d).FirstOrDefault().Kontrol = true;
            db.SaveChanges();
            return Ok();
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DuyuruExists(int id)
        {

            return db.Duyurular.Count(e => e.DuyuruId == id) > 0;
        }

        private string GetRole(string id)
        {
            UserManager<Kullanici> userManager;
            UserStore<Kullanici> userStore = new UserStore<Kullanici>(db);
            userManager = new UserManager<Kullanici>(userStore);
            return userManager.GetRoles(id).FirstOrDefault();
        }


    }
}

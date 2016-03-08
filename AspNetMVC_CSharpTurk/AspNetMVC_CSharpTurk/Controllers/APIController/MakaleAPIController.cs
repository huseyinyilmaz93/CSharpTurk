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
    public class MakaleAPIController : ApiController
    {
        private DatabaseContextClass db = new DatabaseContextClass();
        // GET api/MakaleAPI [LIST Makale]

        [HttpGet]
        [Route("api/MakaleAPI")]
        public IEnumerable<MakaleExtraModel> GetMakaleler()
        {
            return (from makale in db.Makaleler
                    orderby makale.MakaleTarih descending
                    select new MakaleExtraModel()
                    {
                        Ad = makale.Yazar.Ad,
                        KullaniciAdi = makale.Yazar.UserName,
                        MakaleBaslik = makale.MakaleBaslik,
                        MakaleId = makale.MakaleId,
                        MakaleOzet = makale.MakaleOzet,
                        MakaleTarih = makale.MakaleTarih,
                        Soyad = makale.Yazar.Soyad,
                        Yazar = makale.Yazar,
                        Etiketler = makale.Etiketler,
                        MakaleUrl = makale.MakaleUrl
                    }).ToList();
        }
        [HttpGet]
        [Route("api/MakaleAPI/Guncel30")]
        public IEnumerable<MakaleExtraModel> Guncel30()
        {
            return (from makale in db.Makaleler.Include("Yazar")
                    orderby makale.MakaleTarih descending
                    select new MakaleExtraModel()
                    {
                        Ad = makale.Yazar.Ad,
                        KullaniciAdi = makale.Yazar.UserName,
                        MakaleBaslik = makale.MakaleBaslik,
                        MakaleId = makale.MakaleId,
                        MakaleOzet = makale.MakaleOzet,
                        MakaleTarih = makale.MakaleTarih,
                        Soyad = makale.Yazar.Soyad,
                        Yazar = makale.Yazar,
                        Etiketler = makale.Etiketler,
                        MakaleUrl = makale.MakaleUrl
                    }  ).Take(30).ToList();
        }
        // GET api/MakaleAPI/5 [GET Makale]
        //[ResponseType(typeof(Makale))] IHttpActionResult
        [HttpGet]
        [Route("api/MakaleAPI/{id}")]
        public Object GetMakale(int id)
        {
            return (from makale in db.Makaleler
                    where makale.MakaleId == id
                    select new {
                        MakaleBaslik = makale.MakaleBaslik,
                        MakaleGovde = makale.MakaleGovde,
                        MakaleOzet = makale.MakaleOzet,
                        MakaleTip = makale.MakaleTipi.MakaleTipAdi,
                    });
        }

        //GET api/MakaleAPI/asd6871dasd-asdhgawe8-uwuer-23 [GET YazarMakale]
        [HttpGet]
        [Route("api/MakaleAPI/YazaraGore/{yazarId}")]
        public IEnumerable<MakaleExtraModel> GetMakale(string yazarId)
        {
            return (from makale in db.Makaleler
                    where makale.Yazar.Id == yazarId
                    orderby makale.MakaleTarih descending
                    select new MakaleExtraModel()
                    {
                        Ad = makale.Yazar.Ad,
                        KullaniciAdi = makale.Yazar.UserName,
                        MakaleBaslik = makale.MakaleBaslik,
                        MakaleId = makale.MakaleId,
                        MakaleOzet = makale.MakaleOzet,
                        MakaleTarih = makale.MakaleTarih,
                        Soyad = makale.Yazar.Soyad,
                        Yazar = makale.Yazar,
                        Etiketler = makale.Etiketler,
                        MakaleUrl = makale.MakaleUrl
                    }).ToList();
        }

        // PUT api/MakaleAPI/5 [EDIT Makale]
        [Authorize(Roles="Yazar,Admin")]
        [HttpPut]
        [Route("api/MakaleAPI/{id}/{yazarId}")]
        public HttpResponseMessage PutMakale(int id, string yazarId, MakalePostModel makale)
        {
            if (!ModelState.IsValid || makale == null)
            {
                if (makale == null) ModelState.AddModelError("formMakaleDuzenle", "Tüm alanlar boş bırakılamaz !");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            Makale gecerliMakale = new Makale();
            //Makalenin sahibi yada admin makaleyi değiştirebilir.

            MakalePostModel makaleKontrol = db.Makaleler.Where(m => m.MakaleId == id && m.Yazar.Id == yazarId).
                Select(m => new MakalePostModel
                {
                    MakaleBaslik = m.MakaleBaslik,
                    MakaleGovde = m.MakaleGovde,
                    MakaleOzet = m.MakaleOzet,
                    MakaleTipi = m.MakaleTipi.MakaleTipAdi
                }).First();

            if ( this.GetRole(yazarId) == null) return Request.CreateErrorResponse(HttpStatusCode.Forbidden,new HttpError());
            if (makaleKontrol == null) return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, new HttpError());


            db.Makaleler.Where(m => m.MakaleId == id).Select(m => m).First().MakaleTipi = 
                db.MakaleTipleri.Where(t => t.MakaleTipAdi == makale.MakaleTipi).First();


            bool hasChanged = (makaleKontrol.MakaleBaslik != makale.MakaleBaslik ||
                makaleKontrol.MakaleGovde != makale.MakaleGovde ||
                makaleKontrol.MakaleOzet != makale.MakaleOzet || makaleKontrol.MakaleTipi != makale.MakaleTipi ? true : false);

            if (hasChanged)
            {
                Makale yeniMakale = db.Makaleler.Where(m => m.MakaleId == id).FirstOrDefault();
                yeniMakale.MakaleBaslik = makale.MakaleBaslik;
                yeniMakale.MakaleGovde = makale.MakaleGovde;
                yeniMakale.MakaleOzet = makale.MakaleOzet;
                yeniMakale.MakaleTipi = db.MakaleTipleri.Where(t=>t.MakaleTipAdi == makale.MakaleTipi).FirstOrDefault();
            }
            else return Request.CreateResponse(HttpStatusCode.NoContent);

            try
            {
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakaleExists(id))
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
            }
        }

        [Authorize(Roles = "Yazar,Admin")]
        // POST api/MakaleAPI [CREATE Makale]
        [HttpPost]
        [Route("api/MakaleAPI/{yazarId}")]
        [ResponseType(typeof(MakalePostModel))]
        public HttpResponseMessage PostMakale(MakalePostModel model, string yazarId)
        {
            Makale makale = new Makale();
            if (!ModelState.IsValid || model == null)
            {
                if(model == null) ModelState.AddModelError("formMakaleOlustur", "Tüm alanlar boş bırakılamaz !");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if(model.MakaleTipi == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Lütfen makale tipi seçin!");
            try
            {
                makale.MakaleBaslik = model.MakaleBaslik;
                makale.MakaleGovde = model.MakaleGovde;
                makale.MakaleOzet = model.MakaleOzet;
                makale.MakaleTarih = DateTime.Now;
                makale.MakaleTipi = db.MakaleTipleri.Where(t => t.MakaleTipAdi.Equals(model.MakaleTipi)).FirstOrDefault();

                var kategori = db.Kategoriler.Where(k => k.KategoriAdi == "Makale").Select(k => k).FirstOrDefault();
                var yazar = db.Users.Where(u => u.Id == yazarId).Select(u => u).FirstOrDefault();
                makale.Kategori = kategori;
                makale.Yazar = yazar;

                db.Makaleler.Add(makale);
                db.SaveChanges();
                makale.MakaleUrl = makale.MakaleBaslik.Replace(" ", "-").ToLower().Replace("ö", "o").
                    Replace("ğ","g").Replace("ç","c").Replace("ü","u").Replace("ş","s") + "-" + makale.MakaleId;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK,makale.MakaleId);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadGateway,new HttpError());
                //return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

            //return StatusCode(HttpStatusCode.BadRequest);
        }

        [Authorize(Roles = "Yazar,Admin")]
        // DELETE api/MakaleAPI/5 [DELETE Makale]
        [HttpDelete]
        [Route("api/MakaleAPI/{id}/{yazarId}")]
        [ResponseType(typeof(Makale))]
        public IHttpActionResult DeleteMakale(int id, string yazarId)
        {
            Makale makale = db.Makaleler.Find(id);

            //Makalenin sahibi yada admin makaleyi silebilir.
            Makale makaleKontrol = db.Makaleler.Where(m => m.MakaleId == id && m.Yazar.Id == yazarId).Select(m => m).FirstOrDefault();

            if (makaleKontrol == null && this.GetRole(yazarId) != "Admin")
            {
                return StatusCode(HttpStatusCode.Ambiguous);
            }


            if (makale == null)
            {
                return NotFound();
            }

            db.Makaleler.Remove(makale);
            db.SaveChanges();

            return Ok(makale);
        }

        [HttpGet]
        [AcceptVerbs("GET")]
        [Route("api/MakaleAPI/Ara/{aramaFiltresi}")]
        public IEnumerable<Makale> Arama(string aramaFiltresi)
        {
            List<Makale> aramaSonucu = new List<Makale>();
            foreach (var makale in db.Makaleler)
            {
                if (makale.MakaleBaslik.Contains(aramaFiltresi))
                {
                    makale.MakaleBaslik = makale.MakaleBaslik.Replace(aramaFiltresi, string.Concat("<span style='background-color:yellow'>", aramaFiltresi, "</span>"));
                    aramaSonucu.Add(makale);
                    
                }
                if (makale.MakaleGovde.Contains(aramaFiltresi))
                {
                    aramaSonucu.Add(makale);
                }

                if (makale.MakaleOzet.Contains(aramaFiltresi))
                {
                    makale.MakaleOzet = makale.MakaleOzet.Replace(aramaFiltresi, string.Concat(@"<span style='background-color:yellow'>", aramaFiltresi, "</span>"));
                    aramaSonucu.Add(makale);
                }
            }
            return aramaSonucu.Distinct();
        }


        [HttpGet]
        [Route("api/MakaleAPI/MakaleTipleri")]
        public IEnumerable<MakaleTip> MakaleTipleri()
        {
            return db.MakaleTipleri.ToList();
        }

        [HttpGet]
        [Route("api/MakaleAPI/MakaleTip/{tipId}")]
        public IEnumerable<MakaleExtraModel> MakaleTip(int tipId)
        {
            return (from makale in db.Makaleler
                    where makale.MakaleTipi.MakaleTipId.Equals(tipId)
                    select new MakaleExtraModel
                    {
                        Ad = makale.Yazar.Ad,
                        KullaniciAdi = makale.Yazar.UserName,
                        MakaleBaslik = makale.MakaleBaslik,
                        MakaleId = makale.MakaleId,
                        MakaleOzet = makale.MakaleOzet,
                        MakaleTarih = makale.MakaleTarih,
                        Soyad = makale.Yazar.Soyad,
                        Yazar = makale.Yazar,
                        Etiketler = makale.Etiketler,
                        MakaleUrl = makale.MakaleUrl

                    }).ToList();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MakaleExists(int id)
        {
            return db.Makaleler.Count(e => e.MakaleId == id) > 0;
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

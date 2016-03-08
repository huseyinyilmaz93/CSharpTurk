using AspNetMVC_CSharpTurk.DAL;
using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    public class YorumController : ApiController
    {
        [RoutePrefix("api/YorumAPI")]
        public class YorumAPIController : ApiController
        {
            private DatabaseContextClass db = new DatabaseContextClass();

            // GET api/YorumAPI [x Makalesine ait tüm ONAY'lanmış yorumlar gönderir.]
            [HttpGet]
            [Route("{makaleId}")]
            public IEnumerable<YorumSendModel> GetYorumlar(int makaleId)
            {
                IEnumerable<YorumSendModel> yorumlar = (from yorum in db.Yorumlar
                                                        join makale in db.Makaleler on yorum.Makale.MakaleId equals makale.MakaleId
                                                        where makale.MakaleId == makaleId && yorum.Onay == true
                                                        select new YorumSendModel
                                                        {
                                                            Eposta = yorum.Eposta,
                                                            YorumGovde = yorum.YorumGovde,
                                                            YorumId = yorum.YorumId,
                                                            YorumTarih = yorum.YorumTarih,
                                                            YorumYazanIsim = yorum.YorumYazanIsim
                                                        });

                return yorumlar.ToList();
            }

            //[CREATE Yorum]
            [System.Web.Http.HttpPost]
            [Route("{makaleId}")]
            [ResponseType(typeof(YorumPostModel))]
            public IHttpActionResult PostYorum(YorumPostModel model, int makaleId)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Makale makale;
                Yorum yorum;
                try
                {
                    Kategori catYorum = db.Kategoriler.Where(k => k.KategoriAdi == "Yorum").Select(y => y).FirstOrDefault();
                    makale = db.Makaleler.Where(m => m.MakaleId == makaleId).Select(m => m).FirstOrDefault();
                    yorum = new Yorum()
                    {
                        Eposta = model.Eposta,
                        Kontrol = false,
                        Makale = makale,
                        Onay = false,
                        YorumGovde = model.YorumGovde,
                        YorumTarih = DateTime.Now,
                        YorumYazanIsim = model.YorumYazanIsim,
                        Kategori = catYorum,
                    };
                }
                catch (Exception)
                {
                    return InternalServerError();
                }

                db.Yorumlar.Add(yorum);
                db.SaveChanges();

                return Ok();
            }

            //[DELETE Yorum]
            [System.Web.Http.HttpDelete]
            [Route("{yorumId}")]
            [ResponseType(typeof(Yorum))]
            public IHttpActionResult DeleteYorum(int yorumId)
            {
                Yorum yorum = db.Yorumlar.Find(yorumId);
                if (yorum == null)
                {
                    return NotFound();
                }

                db.Yorumlar.Remove(yorum);
                db.SaveChanges();

                return Ok(yorum);
            }

            //Yorumları onay veya red için tüm yorumları getirir.
            [System.Web.Http.HttpGet]
            [Route("Kontrol")]
            public IEnumerable<Yorum> GetYorumKontrol()
            {
                return db.Yorumlar.Where(y => y.Kontrol == false).Select(y => y).ToList();
            }

            [System.Web.Http.HttpHead]
            [Route("Onay/{id}")]
            public IHttpActionResult YorumOnayla(int id)
            {
                db.Yorumlar.Where(y => y.YorumId == id).Select(y => y).FirstOrDefault().Onay = true;
                db.Yorumlar.Where(y => y.YorumId == id).Select(y => y).FirstOrDefault().Kontrol = true;
                db.SaveChanges();
                return Ok();
            }

            [System.Web.Http.HttpGet]
            [System.Web.Http.Route("Onaylanacak")]
            public int Onaylanacak()
            {
                return db.Yorumlar.Where(y => y.Kontrol == false).Select(y => y).ToList().Count;

            }

            [System.Web.Http.HttpHead]
            [System.Web.Http.Route("Red/{id}")]
            public IHttpActionResult DuyuruReddet(int id)
            {
                db.Yorumlar.Where(y => y.YorumId == id).Select(y => y).FirstOrDefault().Onay = false;
                db.Yorumlar.Where(y => y.YorumId == id).Select(y => y).FirstOrDefault().Kontrol = true;
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

            private bool YorumExists(int id)
            {
                return db.Yorumlar.Count(e => e.YorumId == id) > 0;
            }
        }
    }
}

using AspNetMVC_CSharpTurk.DAL;
using AspNetMVC_CSharpTurk.Models.AccountModels;
using AspNetMVC_CSharpTurk.Models.MailModels;
using AspNetMVC_CSharpTurk.Models.MembershipModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    public class HesapAPIController : ApiController
    {
        DatabaseContextClass db = new DatabaseContextClass();
        UserStore<Kullanici> userStore { get; set; }
        UserManager<Kullanici> userManager { get; set; }
        public HesapAPIController()
        {
            userStore = new UserStore<Kullanici>(db);
            userManager = new UserManager<Kullanici>(userStore);
            //var provider = new DpapiDataProtectionProvider();

        }
        [HttpGet]
        [Route("api/HesapAPI/Deneme")]
        public HttpResponseMessage Deneme()
        {
            Kullanici user = userManager.FindByEmail("huseyin.yilmaz093@gmail.com");
            userManager.GeneratePasswordResetToken(user.Id);

            return null;
        }
        [HttpGet]
        [Route("api/HesapAPI")]
        public IEnumerable<Kullanici> GetYazarlar()
        {
            string id = db.Roles.Where(r => r.Name == "Admin").Select(r=>r.Id).FirstOrDefault();
            List<Kullanici> yazarlar = new List<Kullanici>();
            foreach (var yazar in db.Users)
            {
                if (yazar.UserName == "admin") continue;
                yazarlar.Add(yazar);
            }
            return yazarlar;
        }
        [HttpPost]
        [ResponseType(typeof(LoginPostModel))]
        [Route("api/HesapAPI/GirisYap")]
        public HttpResponseMessage GirisYap(LoginPostModel model)
        {
            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("Login", "Kullanıcı adı ve şifre giriniz");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState); 
            }
            Kullanici user; 
            try
            {
                user = userManager.Find(model.UserName, model.Password);
            }
            catch (ArgumentNullException)
            {
                ModelState.AddModelError("Login", "Kullanıcı adı veya şifre alanları boş geçilemez.");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);

            }

            if (user == null)
            {
                ModelState.AddModelError("Login","Kullanıcı adı veya şifre yanlış. Tekrar deneyin");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.OK,user);
            }
        }
        [HttpPost]
        [Route("api/HesapAPI/Kayit/{resim}")]
        public HttpResponseMessage KullaniciKayit(string resim,Kayit model)
        {
            if (!ModelState.IsValid || model == null)
            {
                if(model == null) ModelState.AddModelError("Kayit", "Form bilgileri boş girilemez.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (userManager.FindByName(model.UserName) != null)
	        {
                ModelState.AddModelError("Kayıt", "Bu kullanıcı adı başka bir kullanıcı tarafından kullanılmaktadır!");
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);

	        } 
            else if(userManager.FindByEmail(model.EMail) != null)
            {
                ModelState.AddModelError("Kayıt", "Girilen Email başka bir kullanıcı tarafından kullanılmaktadır!");
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);
            }

            try
            {
                Kullanici user = new Kullanici();
                user.Ad = model.Name;
                user.Soyad = model.Surname;
                user.Email = model.EMail;
                user.UserName = model.UserName;
                user.WebSite = model.WebSite;
                if (resim == "null" || resim == "undefined")
                {
                    user.ResimURL = "/Files/KullaniciResimleri/default.jpg";
                }
                else
                {
                    string uzanti = resim.Substring(0, resim.IndexOf("-"));
                    string resimAdi = resim.Substring(resim.IndexOf("-") + 1);

                    user.ResimURL = string.Concat("/Files/KullaniciResimleri/", resimAdi, ".", uzanti);
                }

                IdentityResult idtResult = userManager.Create(user, model.Password);
                if (idtResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Yazar");
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    ModelState.AddModelError("Kayıt","Kullanıcıya rol atanamadı!");
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,ModelState);
                }
            }
            catch (Exception)
            {
                ModelState.AddModelError("Kayıt", "Kullanıcıyı kaydederken bir hata meydana geldi.");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
        }
        [HttpPost]
        [Route("api/HesapAPI/SifreDegistir/{yazarId}")]
        public HttpResponseMessage SifreDegistir(SifreDegisim model, string yazarId)
        {
            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("SifreDegisim", "Tüm alanlar boş girilemez!");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest,ModelState);
            }

            try
            {
                Kullanici kullanici = userManager.FindById(yazarId);
                Kullanici deneme = userManager.Find(kullanici.UserName, model.Sifre);
                if (deneme == null)
                {
                    ModelState.AddModelError("SifreDegisim", "Kullanıcıya ait şifreyi kontrol edin.");
                    return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, ModelState);
                }
                userManager.ChangePassword(yazarId, model.Sifre, model.YeniSifre);
                return Request.CreateResponse(HttpStatusCode.OK,userManager.GetRoles(yazarId).FirstOrDefault());
            }
            catch (Exception)
            {
                ModelState.AddModelError("SifreDegisim", "Kullanıcıya ait şifre yanlış girilmiş.");
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable,new HttpError());
            }
        }
        [HttpGet]
        [Route("api/HesapAPI/{kullaniciId}")]
        public HttpResponseMessage GetKullanici(string kullaniciId)
        {
            if(kullaniciId == "undefined")
	        {
                ModelState.AddModelError("Kullanıcı Hata", "Kullanıcı id değeri hatalı!");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
	        }
            else if (kullaniciId != null)
            {       
                Kullanici user = userManager.FindById(kullaniciId);
                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            else
            {
                ModelState.AddModelError("Kullanıcı Hata", "Verilen Id değerinde kullanıcı bulunamadı");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
            
        }
        [HttpPut]
        [Route("api/HesapAPI/YazarDuzenle/{yazarId}/{resim}")]
        public HttpResponseMessage YazarDuzenle(string yazarId,string resim, YazarDuzenleModel model)
        {
            Kullanici yazar = userManager.FindById(yazarId);
            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("FormDuzenle", "Tüm alanlar boş geçilemez!");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (yazar == null)
            {
                ModelState.AddModelError("Yazar", "Verilen id değerine ait yazar bulunamadı!");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else if (resim == "undefined" || resim == null)
            {
                ModelState.AddModelError("Resim", "Gönderilen resim ile ilgili bir hata meydana geldi! Gönderilen resmi kontrol edin.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (resim != "null")
            {
                string uzanti = resim.Substring(0, resim.IndexOf("-"));
                string resimAdi = resim.Substring(resim.IndexOf("-") + 1);
                string resimURL = string.Concat("/Files/KullaniciResimleri/", resimAdi, ".", uzanti);
                if (yazar.ResimURL != resimURL)
                {
                    db.Users.Where(u => u.Id == yazarId).Select(u => u).First().ResimURL = resimURL;
                }
            }

            bool hasChanged = (yazar.Ad != model.Name || yazar.Soyad != model.Surname ||
               yazar.Email != model.EMail || yazar.WebSite != model.WebSite  ) ? true : false;

            if (hasChanged)
            {
                db.Users.Where(u => u.Id == yazarId).Select(u => u).First().Ad = model.Name;
                db.Users.Where(u => u.Id == yazarId).Select(u => u).First().Soyad = model.Surname;
                db.Users.Where(u => u.Id == yazarId).Select(u => u).First().WebSite = model.WebSite;
                db.Users.Where(u => u.Id == yazarId).Select(u => u).First().Email = model.EMail;
            }  
            try
            {
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("Veritabanı Hatası", "Veritabanına kaydederken hata oluştu!");
                return Request.CreateErrorResponse(HttpStatusCode.Forbidden, ModelState);
            }
        }
        [HttpHead]
        [Route("api/HesapAPI/YazarSil/{yazarId}")]
        public HttpResponseMessage YazarSil(string yazarId)
        {
            try
            {
                Kullanici silinecek = userManager.FindById(yazarId);
                userManager.Delete(silinecek);
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Veritabanı Hatası", "Veritabanında hata meydana geldi");
                return Request.CreateErrorResponse(HttpStatusCode.MethodNotAllowed, ModelState);
            }
            
        }
        [HttpGet]
        [Route("api/HesapAPI/ResimDegistir/{yazarId}/{resim}")]
        public HttpResponseMessage ResimDegistir(string yazarId, string resim)
        {
            Kullanici user = userManager.FindById(yazarId);
            if (user == null)
            {
                ModelState.AddModelError("Yazar", "Verilen id değerine ait yazar bulunamadı!");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);

            }
            else if (resim == "null" || resim == "undefined" || resim == null)
            {
                ModelState.AddModelError("Resim", "Gönderilen resim ile ilgili bir hata meydana geldi! Gönderilen resmi kontrol edin.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            string uzanti = resim.Substring(0, resim.IndexOf("-"));
            string resimAdi = resim.Substring(resim.IndexOf("-") + 1);

            string resimURL = string.Concat("/Files/KullaniciResimleri/", resimAdi, ".", uzanti);

            if (user.ResimURL == resimURL)
            {
                ModelState.AddModelError("Resim Aynı", "Gönderilen resim ile kayıtlı resim aynı.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                
                db.Users.Where(u => u.Id == yazarId).Select(u => u).First().ResimURL = resimURL;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                ModelState.AddModelError("Veritabanı", "Resmi kaydederken bir hata meydana geldi.");
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ModelState);
            }

        }

        [HttpPost]
        [Route("api/HesapAPI/MailKontrol")]
        public HttpResponseMessage MailKontrol(Mail mail)
        {
            Kullanici user = userManager.FindByEmail(mail.email);
            if ( user == null)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Verilen e-mail adresine ait kayıt bulunamadı!");
            try
            {
                user.SifremiUnuttum = DateTime.Now;
                db.SaveChanges();
                MailGonderme.MailGonder(user.Id,new System.Net.Mail.MailAddress(user.Email, string.Concat(user.Ad, " ", user.Soyad)));
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotImplemented, "Veritabanına kaydederken bir hata meydana geldi!");
            }
        }


        [HttpGet]
        [Route("api/HesapAPI/SifremiUnuttum/{yazarId}")]
        public HttpResponseMessage SifremiUnuttum(string yazarId)
        {
            Kullanici user = userManager.FindById(yazarId);
            
            if(DateTime.Compare(user.SifremiUnuttum.AddHours(3),DateTime.Now)>= 0) 
                return Request.CreateResponse(HttpStatusCode.OK);
            else return Request.CreateErrorResponse(HttpStatusCode.BadRequest,"İstek zaman aşımına uğradı!");
        }


        [HttpPost]
        [Route("api/HesapAPI/SifremiUnuttumDegisim/{yazarId}")]
        public HttpResponseMessage SifremiUnuttumDegisim(SifremiUnuttum model, string yazarId)
        {
            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("SifreDegisim", "Tüm alanlar boş girilemez!");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            try
            {
                Kullanici kullanici = userManager.FindById(yazarId);
                String hashSifre = userManager.PasswordHasher.HashPassword(model.YeniSifre);
                Task t = (userStore.SetPasswordHashAsync(kullanici, hashSifre));
                t.Wait();
                t = (userStore.UpdateAsync(kullanici));
                t.Wait();
                kullanici.SifremiUnuttum = kullanici.SifremiUnuttum.AddHours(-3);
                return Request.CreateResponse(HttpStatusCode.OK, userManager.GetRoles(yazarId).FirstOrDefault());
            }
            catch (Exception e)
            {
                ModelState.AddModelError("SifreDegisim", "Kullanıcıya ait şifre yanlış girilmiş.");
                return Request.CreateErrorResponse(HttpStatusCode.NotAcceptable, e.Message);
            }
        }


        private bool IsYazar(string id)
        {
            UserManager<Kullanici> userManager;
            UserStore<Kullanici> userStore = new UserStore<Kullanici>(db);
            userManager = new UserManager<Kullanici>(userStore);
            return userManager.GetRoles(id).FirstOrDefault() == "Yazar";
        }
    }
}

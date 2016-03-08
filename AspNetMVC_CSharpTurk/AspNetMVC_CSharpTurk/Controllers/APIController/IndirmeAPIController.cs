using AspNetMVC_CSharpTurk.DAL;
using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    public class IndirmeAPIController : ApiController
    {
        DatabaseContextClass db = new DatabaseContextClass();
        [Route("api/IndirmeAPI/{dosya}")]
        [HttpPost]
        public HttpResponseMessage IndirmeOlustur(string yazarId,string dosya,IndirmePostModel model)
        {
            Kullanici yazar = db.Users.Where(k => k.Id == yazarId).FirstOrDefault();

            if (!ModelState.IsValid || model == null)
            {
                if (model == null) ModelState.AddModelError("Form", "Tüm elemanlar boş girilemez.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (yazar == null)
            {
                ModelState.AddModelError("Yazar", "Verilen id değerine ait yazar bulunamadı!");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else if (dosya == "undefined" || dosya == null)
            {
                ModelState.AddModelError("Dosya", "Gönderilen dosya ile ilgili bir hata meydana geldi! Gönderilen resmi kontrol edin.");
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
            if (dosya != "null")
            {
                string uzanti = dosya.Substring(0, dosya.IndexOf("-"));
                string resimAdi = dosya.Substring(dosya.IndexOf("-") + 1);
                string resimURL = string.Concat("/Files/Indirmeler/", resimAdi, ".", uzanti);
                if (yazar.ResimURL != resimURL)
                {
                    db.Users.Where(u => u.Id == yazarId).Select(u => u).First().ResimURL = resimURL;
                }
            }
            return null;


        }
    }
}

using AspNetMVC_CSharpTurk.DAL;
using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using AspNetMVC_CSharpTurk.Models.ModelBinder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    [Authorize(Roles="Yazar,Admin")]
    public class EtiketAPIController : ApiController
    {
        DatabaseContextClass db = new DatabaseContextClass();

        [HttpGet,Route("api/EtiketAPI/Etiketler")]
        public IEnumerable<Etiket> Etiketler()
        {
            return db.Etiketler.ToList();
        }

        [HttpPost, Route("api/EtiketAPI/EtiketEkle/{makaleId}")]
        public HttpResponseMessage EtiketEkle(int makaleId,[FromUri]int[] etiketIds)
        {
            Makale makale = db.Makaleler.Where(m => m.MakaleId == makaleId).FirstOrDefault();
            if (makale == null) Request.CreateErrorResponse(HttpStatusCode.NotFound, "Girilen makale Id için geçerli makale bulunamadı!");
            try
            {
                foreach (var etiketId in etiketIds)
                    makale.Etiketler.Add(db.Etiketler.Where(e => e.EtiketId == etiketId).FirstOrDefault());

                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Veritabanına veri yazarken hata meydana geldi");
            }
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpGet, Route("api/EtiketAPI/GetMakaleEtiket/{makaleId}")]
        public Object MakaleEtiket(int makaleId)
        {
            return from makale in db.Makaleler.Where(m => m.MakaleId == makaleId)
                   select new
                   {
                       Etiketler = makale.Etiketler
                   };
        }

        [HttpPut, Route("api/EtiketAPI/EtiketleriDuzenle/{makaleId}")]
        public HttpResponseMessage EtiketleriDuzenle(int makaleId, [FromUri]int[] etiketIds)
        {
            Makale makale = db.Makaleler.Include("Etiketler").Where(m => m.MakaleId == makaleId).FirstOrDefault();
            if (makale == null) Request.CreateErrorResponse(HttpStatusCode.NotFound, "Girilen makale Id için geçerli makale bulunamadı!");
            makale.Etiketler = new List<Etiket>();
            try
            {
                for (int i = 0; i < etiketIds.Length; i++)
			    {
                    int temp = etiketIds[i];
                    makale.Etiketler.Add(db.Etiketler.Where(e => e.EtiketId == temp).FirstOrDefault());
			    } 
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception)
            {
                Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Veritabanına veri yazarken hata meydana geldi");
            }
            return Request.CreateResponse(HttpStatusCode.OK);

        }
    }
}

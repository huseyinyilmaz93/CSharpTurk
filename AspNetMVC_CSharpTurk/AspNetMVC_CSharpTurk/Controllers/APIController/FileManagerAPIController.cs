using AspNetMVC_CSharpTurk.Models;
using AspNetMVC_CSharpTurk.Models.FileManagerModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    [RoutePrefix("api/FileManagerAPI")]
    public class FileManagerAPIController : ApiController
    {
        private FileManager fileManager = new FileManager();

        [HttpGet, Route("KullaniciAnaDizin/{kullaniciAdi}")]
        public HttpResponseMessage KullaniciAnaDizin(string kullaniciAdi)
        {
            Tuple<bool,string> tuple = fileManager.SetRoot(kullaniciAdi); //Burdan root klasörü dönmektedir
            Tuple<bool, HttpStatusCode, string, List<Dosya>> tupleResponse = fileManager.GetKlasorDizin(tuple.Item2);
            if (tupleResponse.Item1)
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);
        }

        [HttpPost, Route("DosyaBilgi")]
        public HttpResponseMessage DosyaBilgi(Yol dosya)
        {
            Tuple<bool, HttpStatusCode, string, List<Dosya>> tupleResponse = fileManager.DosyaBilgi(dosya.dosyaYolu);
            if (tupleResponse.Item1)
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);
        }

        [HttpPost, Route("AltDizin")]
        public HttpResponseMessage AltDizin(Yol dosya)
        {
            Tuple<bool,HttpStatusCode,string,List<Dosya>> tupleResponse = fileManager.GetKlasorDizin(dosya.dosyaYolu);
            if (tupleResponse.Item1)
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);
        }

        [HttpPost,Route("KlasorEkle")]
        public HttpResponseMessage KlasorEkle(KlasorEkle klasor)
        {
            if (klasor.dosyaYolu== null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dosya yolu biçimi hatalı");
            Tuple<bool, HttpStatusCode, string, List<Dosya>> tupleResponse = fileManager.KlasorEkle(klasor.dosyaYolu, klasor.dosyaAdi);
            if (tupleResponse.Item1)
            {
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            }
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);
        }

        [HttpPost, Route("Sil")]
        public HttpResponseMessage Sil(Sil dosya)
        {
            if (dosya.Yol == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dosya yolu biçimi hatalı");
            Tuple<bool, HttpStatusCode, string, List<Dosya>> tupleResponse = fileManager.Sil(dosya.Yol, dosya.DosyaAdi);
            if (tupleResponse.Item1)
            {
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            }
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);

        }

        [HttpPost, Route("Geri")]
        public HttpResponseMessage Geri(Yol dosya)
        {
            Tuple<bool, HttpStatusCode, string, List<Dosya>> tupleResponse = fileManager.GetKlasorDizin(dosya.dosyaYolu);
            if (tupleResponse.Item1)
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);

        }

        [HttpPost,Route("YenidenAdlandir")]
        public HttpResponseMessage YenidenAdlandir(YenidenAdlandir dosya)
        {
            if (dosya.Yol == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dosya yolu biçimi hatalı");
            Tuple<bool,HttpStatusCode,string,List<Dosya>> tupleResponse = fileManager.YenidenAdlandir(dosya.Yol,dosya.EskiIsim, dosya.YeniIsim);
            if (tupleResponse.Item1)
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);
        }

        [HttpPost,Route("DosyaYukle")]
        public HttpResponseMessage DosyaYukle(string dosyaYolu)
        {
            dosyaYolu = dosyaYolu.Replace(".-123-.", "/");
            Tuple<bool, HttpStatusCode, string, List<Dosya>> tupleResponse = fileManager.DosyaYukle(dosyaYolu, System.Web.HttpContext.Current.Request.Files);
            if (tupleResponse.Item1)
                return Request.CreateResponse(tupleResponse.Item2, tupleResponse.Item4);
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);
        }

        [HttpPost,Route("DosyaTasi")]
        public HttpResponseMessage DosyaTasi(DosyaTasi dosya)
        {
            if (dosya.EskiYol == null || dosya.YeniYol == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Dosya yolu biçimi hatalı");
            Tuple<bool, HttpStatusCode, string, List<Dosya>> tupleResponse = fileManager.Tasi(dosya.EskiYol, dosya.YeniYol, dosya.GecerliDizin);
            if (tupleResponse.Item1)
                return Request.CreateResponse(tupleResponse.Item2,tupleResponse.Item4);
            else
                return Request.CreateErrorResponse(tupleResponse.Item2, tupleResponse.Item3);
        }
    }
}

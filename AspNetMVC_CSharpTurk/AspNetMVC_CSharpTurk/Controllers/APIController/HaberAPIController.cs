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
    public class HaberAPIController : ApiController
    {
        private DatabaseContextClass db = new DatabaseContextClass();
        
        [HttpGet,Route("api/HaberAPI/TumHaberler")]
        public IEnumerable<Haber> TumHaberler()
        {
               return (from haber in db.Haberler
                orderby haber.HaberTarih descending
                select haber).ToList();
        }

        [Route("api/HaberAPI/GuncelHaber")]
        [HttpGet]
        public Haber GetGuncelHaber()
        {
            return (from haber in db.Haberler
                   orderby haber.HaberTarih descending
                   select haber).FirstOrDefault();
        }

        [HttpGet]
        [Route("api/HaberAPI/GuncelHaberler")]
        public IEnumerable<Haber> GetHaberler()
        {
            IEnumerable<Haber> haberler =  (from haber in db.Haberler
                    orderby haber.HaberTarih descending
                    select haber).Take(4);
            return haberler.Skip(1);
        }


        [HttpGet]
        [Route("api/HaberAPI/HaberDetay/{haberId}")]
        public Haber TumHaberler(int haberId)
        {
            return db.Haberler.Where(h => h.HaberId == haberId).FirstOrDefault();
        }
    }
}

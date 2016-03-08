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
    public class EtkinlikAPIController : ApiController
    {
        private DatabaseContextClass db = new DatabaseContextClass();

        [HttpGet,Route("api/EtkinlikAPI/TumEtkinlikler")]
        public IEnumerable<Etkinlik> TumEtkinlikler()
        {
            return (from etkinlik in db.Etkinlikler
                    orderby etkinlik.Baslangic descending
                    select etkinlik).ToList();
        }

        [Route("api/EtkinlikAPI/GuncelEtkinlik")]
        [HttpGet]
        public Etkinlik GetGuncelEtkinlik()
        {
            List<Etkinlik> guncelEtkinlikler = (from etk in db.Etkinlikler
                                                where DateTime.Compare(etk.Bitis,DateTime.Now) >= 0
                                                orderby etk.Baslangic
                                                select etk).ToList();

            foreach (var etkinlik in guncelEtkinlikler)
            {
                if (DateTime.Compare(etkinlik.Bitis, DateTime.Now) >= 0) return etkinlik;                
            }
            return null;
        }

        [HttpGet]
        [Route("api/EtkinlikAPI/GuncelEtkinlikler")]
        public IEnumerable<Etkinlik> GetEtkinlikler()
        {
            List<Etkinlik> guncelEtkinlikler = (from etk in db.Etkinlikler
                                                where DateTime.Compare(etk.Bitis, DateTime.Now) >= 0
                                                orderby etk.Baslangic
                                                select etk).ToList();

            List<Etkinlik> etkinlikler = new List<Etkinlik>();
            foreach (var etkinlik in guncelEtkinlikler)
            {
                if (DateTime.Compare(etkinlik.Bitis, DateTime.Now) >= 0)
                {
                    etkinlikler.Add(etkinlik);
                    if (etkinlikler.Count == 4) break;
                }
            }
            if (etkinlikler.Count > 0) return etkinlikler.Skip(1);
            else return null;
        }

        [HttpGet]
        [Route("api/EtkinlikAPI/EtkinlikDetay/{etkinlikId}")]
        public Etkinlik EtkinlikDetay(int etkinlikId)
        {
            return db.Etkinlikler.Where(e => e.EtkinlikId == etkinlikId).FirstOrDefault();
        }
    }
}

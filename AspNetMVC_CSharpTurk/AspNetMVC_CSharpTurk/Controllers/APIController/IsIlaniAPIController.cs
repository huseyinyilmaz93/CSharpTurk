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
    public class IsIlaniAPIController : ApiController
    {
        private DatabaseContextClass db = new DatabaseContextClass();

        [HttpGet,Route("api/IsIlaniAPI/TumIlanlar")]
        public IEnumerable<IsIlani> TumIlanlar()
        {
            return (from ilan in db.Ilanlar
                    orderby ilan.IlanTarih descending
                    select ilan).ToList();

        }

        [Route("api/IsIlaniAPI/GuncelIlan")]
        [HttpGet]
        public IsIlani GetGuncelIlan()
        {
            return (from ilan in db.Ilanlar
                    orderby ilan.IlanTarih descending
                    select ilan).FirstOrDefault();
        }

        [HttpGet]
        [Route("api/IsIlaniAPI/GuncelIlanlar")]
        public IEnumerable<IsIlani> GetIlanlar()
        {
            IEnumerable<IsIlani> ilanlar = (from ilan in db.Ilanlar
                                            orderby ilan.IlanTarih descending
                                            select ilan).Take(4);
            return ilanlar.Skip(1);
        }

        [HttpGet]
        [Route("api/IsIlaniAPI/IlanDetay/{ilanId}")]
        public IsIlani TumIlanlar(int ilanId)
        {
            return db.Ilanlar.Where(i => i.IlanId == ilanId).FirstOrDefault();
        }

    }
}

using AspNetMVC_CSharpTurk.DAL;
using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNet.Identity;

namespace AspNetMVC_CSharpTurk.Controllers.APIController
{
    public class IpucuAPIController : ApiController
    {
        DatabaseContextClass db = new DatabaseContextClass();
        // GET api/Ipucu [LIST Ipucu]
        public IEnumerable<Ipucu> GetIpuclari()
        {
            
            return db.Ipuclari.ToList();
        }

        // GET api/Ipucu/5 [GET Ipucu]
        //[ResponseType(typeof(Ipucu))]
        public Ipucu GetIpucu(int id)
        {
            var ipucu = db.Ipuclari.Where(d => d.IpucuId == id).Select(d => d);
            if (ipucu.Count() == 0)
            {
                return null;
            }
            return ipucu.FirstOrDefault();
        }

        // PUT api/Ipucu/5 [EDIT Ipucu]
        public IHttpActionResult PutIpucu(int id, IpucuPostModel ipucu)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Ipucu gecerliIpucu = new Ipucu();
            bool hasChanged = (gecerliIpucu.IpucuBaslik != ipucu.IpucuBaslik ||
            gecerliIpucu.IpucuGovde != ipucu.IpucuGovde ? true : false);

            if (hasChanged)
            {
                db.Ipuclari.Where(m => m.IpucuId == id).Select(m => m).First().IpucuBaslik = ipucu.IpucuBaslik;
                db.Ipuclari.Where(m => m.IpucuId == id).Select(m => m).First().IpucuGovde = ipucu.IpucuGovde;
            }
            else return StatusCode(HttpStatusCode.Continue);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IpucuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Ipucu [CREATE Ipucu]
        [ResponseType(typeof(IpucuPostModel))]
        public IHttpActionResult PostIpucu(IpucuPostModel model)
        {
            
            Ipucu ipucu = new Ipucu();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                ipucu.IpucuBaslik = model.IpucuBaslik;
                ipucu.IpucuGovde = model.IpucuGovde;
                ipucu.IpucuTarih = DateTime.Now;

                var kategori = db.Kategoriler.Where(k => k.KategoriAdi == "Ipucu").Select(k => k).FirstOrDefault();
                string userId = User.Identity.GetUserId();
                var yazar = db.Users.Where(u => u.Id == userId).Select(u => u).FirstOrDefault();
                ipucu.Kategori = kategori;
                ipucu.Yazar = yazar;

                db.Ipuclari.Add(ipucu);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }

            return CreatedAtRoute("DefaultApi", new { id = ipucu.IpucuId }, ipucu);
        }

        // DELETE api/Ipucu/5 [DELETE Ipucu]
        [ResponseType(typeof(Ipucu))]
        public IHttpActionResult DeleteIpucu(int id)
        {
            
            Ipucu ipucu = db.Ipuclari.Find(id);
            if (ipucu == null)
            {
                return NotFound();
            }

            db.Ipuclari.Remove(ipucu);
            db.SaveChanges();

            return Ok(ipucu);
        }

        protected override void Dispose(bool disposing)
        {
            
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IpucuExists(int id)
        {
            
            return db.Ipuclari.Count(e => e.IpucuId == id) > 0;
        }
    }
}

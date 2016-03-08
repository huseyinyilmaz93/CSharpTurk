using AspNetMVC_CSharpTurk.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Haber
{
    public class DBYaz
    {
        DatabaseContextClass db = new DatabaseContextClass();
        public void HaberleriAlVeYazdir()
        {
            RssHaber haberAl = new RssHaber();
            var haberler =  haberAl.GetRssFeed();
            foreach (AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Haber haber in haberler)
            {
                if (Karsilastir(haber)) continue;
                haber.Kategori = db.Kategoriler.Where(k => k.KategoriAdi == "Haber").FirstOrDefault();
                haber.Yazar = db.Yazarlar.Where(y => y.UserName == "admin").FirstOrDefault();

                db.Haberler.Add(haber);
                db.SaveChanges();
                haber.HaberUrl = haber.HaberUrl = haber.HaberBaslik.Replace(" ", "-").ToLower().Replace("ö", "o").
                    Replace("ğ", "g").Replace("ç", "c").Replace("ü", "u").Replace("ş", "s") + "-" + haber.HaberId;
            }
            db.SaveChanges();

        }
        public bool Karsilastir(AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Haber haber)
        {
            foreach (var item in db.Haberler)
            {
                if (haber.HaberTarih == item.HaberTarih && haber.HaberBaslik == item.HaberBaslik) 
                return true;
            }
            return false;
        }
    }
}
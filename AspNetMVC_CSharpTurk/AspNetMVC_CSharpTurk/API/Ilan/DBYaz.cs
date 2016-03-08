using AspNetMVC_CSharpTurk.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Ilan
{
    public class DBYaz
    {
        DatabaseContextClass db = new DatabaseContextClass();
        public void IlanleriAlVeYazdir()
        {
            RssIlan ilanAl = new RssIlan();
            var ilanlar = ilanAl.GetRssFeed();
            foreach (AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.IsIlani ilan in ilanlar)
            {
                if (Karsilastir(ilan)) continue;
                ilan.Kategori = db.Kategoriler.Where(k => k.KategoriAdi == "İş İlanı").FirstOrDefault();
                ilan.Yazar = db.Yazarlar.Where(y => y.UserName == "admin").FirstOrDefault();

                db.Ilanlar.Add(ilan);
                db.SaveChanges();
                ilan.IlanUrl = ilan.IlanBaslik.Replace(" ", "-").ToLower().Replace("ö", "o").
                    Replace("ğ", "g").Replace("ç", "c").Replace("ü", "u").Replace("ş", "s") + "-" + ilan.IlanId;
            }
            db.SaveChanges();
        }
        public bool Karsilastir(AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.IsIlani ilan)
        {
            foreach (var item in db.Ilanlar)
            {
                if (ilan.IlanTarih == item.IlanTarih && ilan.IlanBaslik == item.IlanBaslik)
                    return true;
            }
            return false;
        }
    }
}
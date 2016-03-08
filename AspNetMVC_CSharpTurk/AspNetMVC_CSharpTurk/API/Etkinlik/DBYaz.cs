using AspNetMVC_CSharpTurk.API.Etkinlik.Model;
using AspNetMVC_CSharpTurk.DAL;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Etkinlik
{
    public class DBYaz
    {
        DatabaseContextClass db = new DatabaseContextClass();
        public void HaberleriAlVeYazdir()
        {
            ApiEtkinlik etkinlikAl = new ApiEtkinlik();
            List<EtkinlikJSONData> gelenEtkinlikler = etkinlikAl.EtkinlikAl();
            List<AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Etkinlik> etkinlikler =
                new List<Models.DatabaseObjectModels.Etkinlik>();

            foreach (EtkinlikJSONData item in gelenEtkinlikler)
            {
                AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Etkinlik etkinlik =
                    new Models.DatabaseObjectModels.Etkinlik();
                etkinlik.Baslangic = item.baslangic;
                etkinlik.Bitis = item.bitis;
                etkinlik.EtkinlikAdi = item.adi;
                etkinlik.EtkinlikAdres = item.mekan.adi + " " + item.mekan.adresi;
                etkinlik.EtkinlikIcerik = item.icerik;
                etkinlik.EtkinlikIlIlce = item.mekan.ilce.adi + "/" + item.mekan.ilce.sehir.adi;
                etkinlik.EtkinlikioId = item.id;
                etkinlik.EtkinlikRadi = item.radi;
                etkinlik.EtkinlikUrl = item.url;
                etkinlik.ResimURL = ResimGetir(item.url);
                etkinlik.UcretliMi = item.ucretliMi;
                etkinlikler.Add(etkinlik);
            }

            foreach (AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Etkinlik item in etkinlikler)
            {
                item.Kategori = db.Kategoriler.Where(k => k.KategoriAdi == "Etkinlik").FirstOrDefault();
                item.Yazar = db.Yazarlar.Where(y => y.UserName == "admin").FirstOrDefault();

                if(db.Etkinlikler.Where(e=>e.EtkinlikioId == item.EtkinlikioId).Select(e=>e).FirstOrDefault() == null)
                    db.Etkinlikler.Add(item);
            }
            db.SaveChanges();
        }

        private string ResimGetir(string url)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(IcerikGetir(url));
            HtmlNodeCollection img = doc.DocumentNode.SelectNodes("//img");
            List<string> srcs = new List<string>();
            foreach (var item in img)
            {
                if (item.GetAttributeValue("src", "").Contains("PublicDepo/EtkinlikAfis"))
                    return item.GetAttributeValue("src", "");
            }
            return "null";

        }
        private static string IcerikGetir(string urlAddress)
        {
            Uri url = new Uri(urlAddress);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            string html = client.DownloadString(url);
            return html;
        }

    }
}
using AspNetMVC_CSharpTurk.API.Etkinlik.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace AspNetMVC_CSharpTurk.API.Etkinlik
{
    
    public class ApiEtkinlik
    {
        List<string> tags = new List<string>();

        public ApiEtkinlik()
        {
            tags.Add("Kullanıcı Deneyimi");
            tags.Add("Yazılım ve Uygulamalar");
            tags.Add("Teknoloji");

        }

        public List<EtkinlikJSONData> EtkinlikAl()
        {
            string url = "https://etkinlik.io/api/v1/etkinlikler?kategoriId=456";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers.Add("X-ETKINLIK-TOKEN", "XETKINLIK TOKEN GIRILECEKKK");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            Stream resStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(resStream);
            dynamic data = JObject.Parse(reader.ReadToEnd());
            JArray gelenEtkinlikler = data.kayitlar;

            List<EtkinlikJSONData> etkinlikler = new List<EtkinlikJSONData>();
            foreach (var gelenEtkinlik in gelenEtkinlikler)
            {
                EtkinlikJSONData etkinlik = new EtkinlikJSONData();
                etkinlik.id = gelenEtkinlik.Value<int>("id");
                etkinlik.adi = gelenEtkinlik.Value<string>("adi");
                etkinlik.radi = gelenEtkinlik.Value<string>("radi");
                etkinlik.url = gelenEtkinlik.Value<string>("url");
                etkinlik.icerik = gelenEtkinlik.Value<string>("icerik");
                etkinlik.baslangic = gelenEtkinlik.Value<DateTime>("baslangic");
                etkinlik.bitis = gelenEtkinlik.Value<DateTime>("bitis");

                etkinlik.tur = gelenEtkinlik["tur"].ToObject<TurJSONData>();
                etkinlik.ozellik = gelenEtkinlik["ozellik"].ToObject<OzellikJSONData>();
                etkinlik.kategori = gelenEtkinlik["kategori"].ToObject<KategoriJSONData>();

                {
                    JToken JMekan = gelenEtkinlik.SelectToken("mekan");
                    MekanJSONData mekan = new MekanJSONData();
                    mekan.id = JMekan["id"].ToObject<int>();
                    mekan.adi = JMekan["adi"].ToObject<string>();
                    mekan.radi = JMekan["id"].ToObject<string>();
                    mekan.adresi = JMekan["adresi"].ToObject<string>();
                    mekan.konum = JMekan["konum"].ToObject<KonumJSONData>();
                    mekan.ozellik = JMekan["ozellik"].ToObject<MekanaAitOzellikJSONData>();
                    mekan.semt = JMekan["semt"].ToObject<SemtJSONData>();

                    {
                        JToken JIlce = JMekan.SelectToken("ilce");
                        IlceJSONData ilce = new IlceJSONData();
                        ilce.id = JIlce["id"].ToObject<int>();
                        ilce.adi = JIlce["adi"].ToObject<string>();
                        ilce.sehir = JIlce["sehir"].ToObject<SehirJSONData>();
                        mekan.ilce = ilce;
                    }
                    etkinlik.mekan = mekan;
                    //Mekan atandı!
                }
                JToken JEtiketler = gelenEtkinlik.SelectToken("etiketler");
                List<EtiketJSONData> etiketler = new List<EtiketJSONData>();
                foreach (JToken JEtiket in JEtiketler.ToList())
                {
                    EtiketJSONData etiket = new EtiketJSONData();
                    etiket.adi = JEtiket["adi"].ToObject<string>();
                    etiket.radi = JEtiket["radi"].ToObject<string>();
                    etiket.id = JEtiket["id"].ToObject<int>();
                    etiketler.Add(etiket);
                }
                etkinlik.etiketler = etiketler;
                if (Kontrol(etkinlik.etiketler)) etkinlikler.Add(etkinlik);

            }
            return (from et in etkinlikler
                   orderby et.baslangic
                       select et).ToList();
        }

        private bool Kontrol(List<EtiketJSONData> etiketler)
        {
            foreach (var etiket in etiketler) //gelen dataya ait etiketler listesi
            {
                foreach (string tag in tags) //tags => Kabul edilebilir etiketler listesi
                {
                    if (tag.Equals(etiket.adi)) return true;
                }
            }
            return false;
        }
    }
}
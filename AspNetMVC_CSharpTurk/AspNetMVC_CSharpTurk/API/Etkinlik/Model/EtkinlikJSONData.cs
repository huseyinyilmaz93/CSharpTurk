using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Etkinlik.Model
{
    public class EtkinlikJSONData
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string radi { get; set; }
        public string url { get; set; }
        public string icerik { get; set; }
        public DateTime baslangic { get; set; }
        public DateTime bitis { get; set; }
        public bool ucretliMi { get; set; }
        public OzellikJSONData ozellik { get; set; }
        public TurJSONData tur { get; set; }
        public KategoriJSONData kategori { get; set; }
        public MekanJSONData mekan { get; set; }
        public List<EtiketJSONData> etiketler { get; set; }
    }
}
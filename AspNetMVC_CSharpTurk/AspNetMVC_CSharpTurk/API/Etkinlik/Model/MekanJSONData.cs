using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Etkinlik.Model
{
    public class MekanJSONData
    {
        public int id { get; set; }
        public string adi { get; set; }
        public string radi { get; set; }
        public string adresi { get; set; }
        public KonumJSONData konum { get; set; }
        public MekanaAitOzellikJSONData ozellik { get; set; }
        public IlceJSONData ilce { get; set; }
        public SemtJSONData semt { get; set; }
    }
}
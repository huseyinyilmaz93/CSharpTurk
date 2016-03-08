using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Etkinlik.Model
{
    public class IlceJSONData
    {
        public int id { get; set; }
        public string adi { get; set; }
        public SehirJSONData sehir { get; set; }
    }
}
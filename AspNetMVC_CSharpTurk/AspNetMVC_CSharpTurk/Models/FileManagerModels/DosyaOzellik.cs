using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.FileManagerModels
{
    public class DosyaOzellik
    {
        public DateTime OlusturulmaTarihi { get; set; }
        public DateTime DegistirilmeTarihi { get; set; }
        public string Yukseklik { get; set; }
        public string Genislik { get; set; }
        public string Boyut { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.FileManagerModels
{
    public class Dosya
    {
        public string DosyaYolu { get; set; }
        public string DosyaAdi{ get; set; }
        public string DosyaTuru { get; set; }
        public string ResimURL { get; set; }
        public DosyaOzellik DosyaOzellikleri { get; set; }
    }
}
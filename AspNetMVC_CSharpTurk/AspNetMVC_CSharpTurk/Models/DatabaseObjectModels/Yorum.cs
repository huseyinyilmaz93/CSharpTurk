using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{

    public class YorumPostModel
    {
        [Display(Name = "Yorum Sahibi")]
        public string YorumYazanIsim { get; set; }
        [Display(Name = "E-Posta")]
        public string Eposta { get; set; }
        [Display(Name = "Yorum")]
        public string YorumGovde { get; set; }
    }
    public class Yorum
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int YorumId { get; set; }
        [Display(Name = "Yorum Sahibi")]
        public string YorumYazanIsim { get; set; }
        [Display(Name = "E-Posta")]
        public string Eposta { get; set; }
        [Display(Name = "Yorum")]
        public string YorumGovde { get; set; }
        [Display(Name = "Yorum Tarihi")]
        public DateTime YorumTarih { get; set; }
        public bool Onay { get; set; }
        public bool Kontrol { get; set; }
        public virtual Makale Makale { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
    public class YorumSendModel
    {
        public int YorumId { get; set; }
        public string YorumYazanIsim { get; set; }
        public string Eposta { get; set; }
        public string YorumGovde { get; set; }
        public DateTime YorumTarih { get; set; }
    }
}
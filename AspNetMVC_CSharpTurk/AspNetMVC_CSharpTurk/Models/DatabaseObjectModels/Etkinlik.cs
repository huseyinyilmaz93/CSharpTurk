using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class Etkinlik
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int EtkinlikId { get; set; }
        public string EtkinlikRadi { get; set; }
        public string EtkinlikAdi { get; set; }
        public string EtkinlikUrl { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public string EtkinlikIcerik { get; set; }
        public bool UcretliMi { get; set; }
        public string ResimURL { get; set; }
        public int EtkinlikioId { get; set; }
        public string EtkinlikAdres { get; set; }
        public string EtkinlikIlIlce { get; set; }

        public virtual Kategori Kategori { get; set; }
        public virtual Kullanici Yazar { get; set; }
    }
}
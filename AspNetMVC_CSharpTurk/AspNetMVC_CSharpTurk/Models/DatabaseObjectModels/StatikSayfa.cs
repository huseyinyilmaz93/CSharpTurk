using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class StatikSayfaPostModel
    {
        [Display(Name = "Sayfa Adı")]
        [Required]
        public string SayfaAdi { get; set; }
        [Display(Name="İçerik")]
        [Required]
        public string SayfaIcerik { get; set; }
        [Display(Name="Açıklama")]
        [Required]
        public string SayfaAciklama { get; set; }
        [Display(Name="İkon")]
        public string GlyphiconClass { get; set; }

    }

    public class StatikSayfa : StatikSayfaPostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SayfaId { get; set; }
        public string SayfaRadi { get; set; }

        public virtual Kategori Kategori { get; set; }
    }


}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class Indirme : IndirmePostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IndirmeId { get; set; }
        [Required]
        public string IndirmeOzet { get; set; }
        [Required]
        public DateTime IndirmeTarih { get; set; }
        [Required]
        public string IndirmeURL { get; set; }
        public string IndirmeResimURL { get; set; }

        public virtual Kategori Kategori { get; set; }
    }
    public class IndirmePostModel
    {
        [Required(ErrorMessage="Indirme Baslik alanı boş geçilemez.")]
        [Display(Name="Indirme Başlık")]
        public string IndirmeBaslik { get; set; }
        [Required(ErrorMessage="Açıklama alanı boş geçilemez.")]
        [Display(Name="Açıklama")]
        public string IndirmeGovde { get; set; }
    }
}
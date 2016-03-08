using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    
    public class DuyuruPostModel
    {
        [MaxLength(100, ErrorMessage = "Duyuru Başlığı alanı 50 karakterden fazla olamaz!")]
        [Required(ErrorMessage="Duyuru başlığı alanı boş bırakılamaz.")]
        [Display(Name = "Duyuru Başlığı")]
        public string DuyuruBaslik { get; set; }
        [Required(ErrorMessage = "Duyuru için içerik bilgisi girin.")]
        [Display(Name = "İçerik")]
        public string DuyuruGovde { get; set; }
        [Display(Name = "Özet")]
        [Required(ErrorMessage = "Duyuru için özet bilgisi girin.")]
        [MaxLength(200,ErrorMessage = "Özet bilgi alanı 200 karakterden fazla olamaz!")]
        public string DuyuruOzet { get; set; }
    }
    public class Duyuru : DuyuruPostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DuyuruId { get; set; }
        public bool Onay { get; set; }
        public bool Kontrol { get; set; }
        [Display(Name = "Yayınlanma Tarihi")]
        public DateTime DuyuruTarih { get; set; }
        [Display(Name = "Duyuru Resmi")]
        public string DuyuruResimURL { get; set; }
        public virtual Kullanici Yazar { get; set; }
        public virtual Kategori Kategori { get; set; }

    }
}
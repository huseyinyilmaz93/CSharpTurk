using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class MakaleExtraModel
    {
        public int MakaleId { get; set; }
        public string MakaleBaslik { get; set; }
        public DateTime MakaleTarih { get; set; }
        public string MakaleOzet { get; set; }
        public string KullaniciAdi { get; set; }
        public Kullanici Yazar { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string MakaleUrl { get; set; }
        public virtual ICollection<Etiket> Etiketler { get; set; }

    }
    public class MakalePostModel
    {
        [Display(Name = "Makale Başlığı")]
        [MaxLength(100, ErrorMessage = "Makale Başlığı alanı 50 karakterden fazla olamaz!")]
        [Required(ErrorMessage = "Makale Başlığı boş bırakılamaz!")]
        public string MakaleBaslik { get; set; }
        [Display(Name = "Makale Tipi")]
        public string MakaleTipi { get; set; }
        [Display(Name = "İçerik")]
        [Required(ErrorMessage = "İçerik kısmı boş bırakılamaz!")]
        public string MakaleGovde { get; set; } 
        [Display(Name = "Özet Bilgi")]
        [Required(ErrorMessage = "Özet Bilgi kısmı boş bırakılamaz!")]
        public string MakaleOzet { get; set; }
    }

    public class Makale : MakalePostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MakaleId { get; set; }
        [Display(Name = "Yayınlanma Tarihi")]
        [Column(TypeName="datetime2")]
        public DateTime MakaleTarih { get; set; }
        public string MakaleUrl { get; set; }
        public Makale()
        {
            Etiketler = new HashSet<Etiket>();
        }

        public virtual MakaleTip MakaleTipi { get; set; }
        public virtual ICollection<Etiket> Etiketler { get; set; }
        public virtual Kullanici Yazar { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
}
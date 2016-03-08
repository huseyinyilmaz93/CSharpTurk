using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class HataBildir : HataBildirPostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HataId { get; set; }
        public DateTime BildirilmeTarihi { get; set; }
        public bool Kontrol { get; set; }
        public bool Onay { get; set; }
        public virtual Kategori Kategori { get; set; }

    }
    public class HataBildirPostModel
    {
        [Required(ErrorMessage="E-Mail alanı boş geçilemez")]
        [EmailAddress(ErrorMessage = "E-mail adresi geçeli değil")]
        public string Email { get; set; }
        [MaxLength(1000, ErrorMessage = "Hata mesajı 1000 karakterden fazla olamaz!")]
        [Required(ErrorMessage = "Hata mesajı alanı boş geçilemez")]
        public string HataMesaji { get; set; }
    }
}
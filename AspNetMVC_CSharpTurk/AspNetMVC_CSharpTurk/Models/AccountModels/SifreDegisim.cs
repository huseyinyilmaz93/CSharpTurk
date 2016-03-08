using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.AccountModels
{

    public class SifreDegisim
    {
        [MinLength(5,ErrorMessage="Şifre alanı 5 karakterden az olamaz.")]
        [MaxLength(20,ErrorMessage="Şifre alanı 20 karakterden fazla olamaz.")]
        [Required(ErrorMessage="Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; }
        [MinLength(5, ErrorMessage = "Yeni şifre alanı 5 karakterden az olamaz.")]
        [MaxLength(20, ErrorMessage = "Yeni şifre alanı 20 karakterden fazla olamaz.")]
        [Display(Name = "Yeni Şifre")]
        [Required(ErrorMessage = "Yeni şifre alanı boş bırakılamaz.")]
        [Compare("YeniSifreTekrar",ErrorMessage="Şifreler eşleşmiyor")]
        public string YeniSifre { get; set; }
        [MinLength(5, ErrorMessage = "Yeni şifre tekrar alanı 5 karakterden az olamaz.")]
        [MaxLength(20, ErrorMessage = "Yeni şifre tekrar alanı 20 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Yeni şifre tekrar alanı boş bırakılamaz.")]
        [Display(Name = "Yeni Şifre Tekrar")]
        public string YeniSifreTekrar { get; set; }
    }
}
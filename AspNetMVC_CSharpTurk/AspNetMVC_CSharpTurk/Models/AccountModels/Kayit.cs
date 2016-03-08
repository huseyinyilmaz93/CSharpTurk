using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.AccountModels
{


    public class Kayit
    {
        [MaxLength(100,ErrorMessage="Ad alanı 50 karakterden fazla olamaz.")]
        [Required(ErrorMessage="Ad alanı boş geçilemez.")]
        [Display(Name= "Ad")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "Soyad alanı 50 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez.")]
        [Display(Name="Soyad")]
        public string Surname { get; set; }
        [MaxLength(20, ErrorMessage = "Soyad alanı 20 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Kullanıcı Adı alanı boş geçilemez.")]
        [MinLength(5,ErrorMessage="Kullanıcı adı alan 5 karakterden az olamaz. ")]
        [Display(Name="Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Şifre alanı boş geçilemez.")]
        [Display(Name = "Şifre")]
        [MinLength(5, ErrorMessage = "Şifre alanı 5 karakterden az olamaz. ")]
        [MaxLength(20, ErrorMessage = "Şifre alanı 20 karakterden fazla olamaz.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Şifre tekrar alanı boş geçilemez.")]
        [Display(Name="Şifre tekrar")]
        [MinLength(5, ErrorMessage = "Şifre tekrar alanı 5 karakterden az olamaz. ")]
        [MaxLength(20, ErrorMessage = "Şifre tekrar alanı 20 karakterden fazla olamaz.")]
        public string ConfirmPassword { get; set; }
        [MaxLength(50, ErrorMessage = "Web Site alanı 50 karakterden fazla olamaz.")]
        [Display(Name = "Web Site")]
        public string WebSite { get; set; }
        [MaxLength(50, ErrorMessage = "E-Mail alanı 50 karakterden fazla olamaz.")]
        [EmailAddress(ErrorMessage="Girilen veri e-mail formatına uygun değil")]
        [Display(Name = "E-Mail")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        public string EMail { get; set; }
        [Display(Name = "Kullanıcı Resmi")]
        public string ResimURL { get; set; }
    }
    public class YazarDuzenleModel
    {
        [MaxLength(100, ErrorMessage = "Ad alanı 50 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Ad alanı boş geçilemez.")]
        [Display(Name = "Ad")]
        public string Name { get; set; }
        [MaxLength(100, ErrorMessage = "Soyad alanı 50 karakterden fazla olamaz.")]
        [Required(ErrorMessage = "Soyad alanı boş geçilemez.")]
        [Display(Name = "Soyad")]
        public string Surname { get; set; }
        [MaxLength(50, ErrorMessage = "Web Site alanı 50 karakterden fazla olamaz.")]
        [Display(Name = "Web Site")]
        public string WebSite { get; set; }
        [MaxLength(50, ErrorMessage = "E-Mail alanı 50 karakterden fazla olamaz.")]
        [EmailAddress(ErrorMessage = "Girilen veri e-mail formatına uygun değil")]
        [Required(ErrorMessage="Bir e-mail adresi girin ..")]
        [Display(Name = "E-Mail")]
        public string EMail { get; set; }
    }
}
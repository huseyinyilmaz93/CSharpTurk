using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.AccountModels
{
    public class LoginPostModel
    {
        [MaxLength(20,ErrorMessage = "Kullanıcı adı alanı 20 karakterden fazla olamaz.")]
        [MinLength(5,ErrorMessage="Kullanıcı adı alanı 5 karakterden az olamaz.")]
        [Required(ErrorMessage="Kullanıcı adı alanı boş bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı")]
        //[RegularExpression("^[~@#$%^&*|(()){}[]?<>'\\₺€æ@<>.!@#%/]+$", ErrorMessage = "Kullanıcı adı için hatalı karakterleri kullanmayın")]
        public string UserName { get; set; }
        [Display(Name = "Şifre")]
        [MaxLength(20,ErrorMessage =  "Şifre alanı 20 karakterden fazla olamaz.")]
        [Required(ErrorMessage="Şifre alanı boş bırakılamaz.")]
        //[RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid First Name")]
        [MinLength(5,ErrorMessage="Şifre 5 karakterden az olamaz.")]
        public string Password { get; set; }
        [Display(Name = "Beni Hatırla")]
        public bool RememberMe { get; set; }
    }
    public class Login : LoginPostModel
    {
        [Key]
        public int MyProperty { get; set; }
    }
}
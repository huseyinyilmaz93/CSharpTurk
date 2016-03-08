using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.MembershipModels
{
    public class Kullanici : IdentityUser
    {

        public string Ad { get; set; }
        public string Soyad { get; set; }
        [Display(Name="Web Site")]
        public string WebSite { get; set; }
        [Display(Name="Kullanıcı Resmi")]
        public string ResimURL { get; set; }

        public DateTime SifremiUnuttum { get; set; }
    }
}
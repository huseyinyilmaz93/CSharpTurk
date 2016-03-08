using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System.Security.Claims;
using AspNetMVC_CSharpTurk.DAL;
using Microsoft.AspNet.Identity.EntityFramework;
using AspNetMVC_CSharpTurk.Models.MailModels;

namespace AspNetMVC_CSharpTurk.Controllers
{
    public class HesapController : PublicController
    {
        private UserManager<Kullanici> userManager { get; set; }

        public HesapController()
        {
            DatabaseContextClass dbMembership = new DatabaseContextClass();
            UserStore<Kullanici> userStore = new UserStore<Kullanici>(dbMembership);
            userManager = new UserManager<Kullanici>(userStore);
        }

        public ActionResult Deneme()
        {
            return View();
        }
        public ActionResult GirisYap()
        {
            return View();
        }
        [Authorize(Roles = "Yazar,Admin")]
        public ActionResult SifreDegistir()
        {
            ViewBag.yazarId = User.Identity.GetUserId();
            return View();
        }
        public ActionResult SifremiUnuttum(string id)
        {
            ViewBag.yazarId = id;
            return View();
        }

        [HttpPost]
        public string Giris(Yazar model)
        {
            if (model != null)
            {
                //Install-package Microsoft.Owin.Host.SystemWeb
                //GetOwinContext metodu için
                try
                {                    
                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                    Kullanici user = model;
                    ClaimsIdentity identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authProps.IsPersistent = true;
                    authManager.SignIn(authProps, identity);
                }
                catch (Exception)
                {
                    return "OturumHatasi";    
                }

                ViewBag.Result = "Giriş işlemi başarıyla tamamlandı..";

                if (model.UserName == "admin")
                    return "Admin";
                else
                    return "Yazar";
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Böyle bir kullanıcı bulunamadı");
                return "Hata";
            }
        }

        public ActionResult CikisYap()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            ViewBag.Result = "Çıkış yapıldı..";
            return RedirectToAction("Index", "Site");
        }


    }
}
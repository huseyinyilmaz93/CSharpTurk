namespace AspNetMVC_CSharpTurk.Migrations
{
    using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
    using AspNetMVC_CSharpTurk.Models.MembershipModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AspNetMVC_CSharpTurk.DAL.DatabaseContextClass>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(AspNetMVC_CSharpTurk.DAL.DatabaseContextClass context)
        {
            var roleManager = new RoleManager<Rol>(new RoleStore<Rol>(context));
            var userManager = new UserManager<Kullanici>(new UserStore<Kullanici>(context));

            if (!roleManager.RoleExists("Admin"))
            {
                Rol adminRole = new Rol("Admin", "Sistem yöneticisi");
                roleManager.Create(adminRole);
            }

            if (!roleManager.RoleExists("Yazar"))
            {
                Rol yazarRole = new Rol("Yazar", "Site içerisinde makale yayýnlamak için gerekli kullanýcý türü");
                roleManager.Create(yazarRole);
            }

            var user = new Kullanici()
            {
                UserName = "XXX",
                ResimURL = "/Files/KullaniciResimleri/default.jpg"
            };
            IdentityResult idResult = userManager.Create(user, "XXX");

            if (idResult.Succeeded)
            {
                userManager.AddToRole(user.Id, "Admin");
            }

            var hsyn = new Kullanici()
            {
                UserName = "h.ylmz93",
                ResimURL = "/Files/KullaniciResimleri/default.jpg"
            };

            idResult = userManager.Create(hsyn, "XXX");

            if (idResult.Succeeded)
            {
                userManager.AddToRole(hsyn.Id, "Yazar");
            }

            var yunusozen = new Kullanici()
            {
                UserName = "XXX",
                ResimURL = "/Files/KullaniciResimleri/default.jpg"
            };
            idResult = userManager.Create(yunusozen, "XXX");

            if (idResult.Succeeded)
            {
                userManager.AddToRole(yunusozen.Id, "Yazar");
            }

            if (context.Kategoriler.Where(k => k.KategoriAdi == "Makale").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Makale" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Duyuru").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Duyuru" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Görsel Ders").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Görsel Ders" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Yorum").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Yorum" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Indirme").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Indirme" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Ipucu").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Ipucu" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Etkinlik").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Etkinlik" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Haber").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Haber" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Hata").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Hata" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Ýþ Ýlaný").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Ýþ Ýlaný" });
            if (context.Kategoriler.Where(k => k.KategoriAdi == "Statik Sayfa").Select(e => e).FirstOrDefault() == null)
                context.Kategoriler.Add(new Kategori() { KategoriAdi = "Statik Sayfa" });

            if (context.Etiketler.Where(e => e.EtiketAdi == "Microsoft").Select(e=>e).FirstOrDefault() == null)
                context.Etiketler.Add(new Etiket() { EtiketAdi = "Microsoft", EtiketRadi = "microsoft" });
            if (context.Etiketler.Where(e => e.EtiketAdi == ".net").Select(e => e).FirstOrDefault() == null)
                context.Etiketler.Add(new Etiket() { EtiketAdi = ".net", EtiketRadi = "-net" });
            if (context.Etiketler.Where(e => e.EtiketAdi == "MVC").Select(e => e).FirstOrDefault() == null)
                context.Etiketler.Add(new Etiket() { EtiketAdi = "MVC", EtiketRadi = "mvc" });
            if (context.Etiketler.Where(e => e.EtiketAdi == "C#").Select(e => e).FirstOrDefault() == null)
                context.Etiketler.Add(new Etiket() { EtiketAdi = "C#", EtiketRadi = "c#" });
            if (context.Etiketler.Where(e => e.EtiketAdi == "Visual Studio").Select(e => e).FirstOrDefault() == null)
                context.Etiketler.Add(new Etiket() { EtiketAdi = "Visual Studio", EtiketRadi = "visual-studio" });
            if (context.Etiketler.Where(e => e.EtiketAdi == "LINQ").Select(e => e).FirstOrDefault() == null)
                context.Etiketler.Add(new Etiket() { EtiketAdi = "LINQ", EtiketRadi = "linq" });
            if (context.Etiketler.Where(e => e.EtiketAdi == "Mobil Programlama").Select(e => e).FirstOrDefault() == null)
                context.Etiketler.Add(new Etiket() { EtiketAdi = "Mobil Programlama", EtiketRadi = "mobil-programlama" });

            if (context.MakaleTipleri.Where(m => m.MakaleTipAdi == "Baslangic Rehberi").Select(m => m).FirstOrDefault() == null)
                context.MakaleTipleri.Add(new MakaleTip() { MakaleTipAdi = "Baslangic Rehberi" });

            if (context.MakaleTipleri.Where(m => m.MakaleTipAdi == "Windows Mobile").Select(m => m).FirstOrDefault() == null)
                context.MakaleTipleri.Add(new MakaleTip() { MakaleTipAdi = "Windows Mobile" });

            if (context.MakaleTipleri.Where(m => m.MakaleTipAdi == "Parallel Programlama").Select(m => m).FirstOrDefault() == null)
                context.MakaleTipleri.Add(new MakaleTip() { MakaleTipAdi = "Parallel Programlama" });

            if (context.MakaleTipleri.Where(m => m.MakaleTipAdi == "Windows Azure").Select(m => m).FirstOrDefault() == null)
                context.MakaleTipleri.Add(new MakaleTip() { MakaleTipAdi = "Windows Azure" });

            if (context.MakaleTipleri.Where(m => m.MakaleTipAdi == "Ýleri Programlama").Select(m => m).FirstOrDefault() == null)
                context.MakaleTipleri.Add(new MakaleTip() { MakaleTipAdi = "Ýleri Programlama" });

            context.SaveChanges();
        }
    }
}

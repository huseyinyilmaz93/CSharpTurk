using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using AspNetMVC_CSharpTurk.Models.MembershipModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.DAL
{
    public class DatabaseContextClass : IdentityDbContext<Kullanici>
    {
        public DbSet<Yazar> Yazarlar { get; set; }
        public DbSet<Makale> Makaleler { get; set; }
        public DbSet<Yorum> Yorumlar { get; set; }
        public DbSet<Duyuru> Duyurular { get; set; }
        public DbSet<HataBildir> Hatalar { get; set; }
        public DbSet<StatikSayfa> StatikSayfalar { get; set; }
        public DbSet<Haber> Haberler { get; set; }
        public DbSet<Etkinlik> Etkinlikler { get; set; }
        public DbSet<GorselDers> GorselDersler { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Indirme> Indirmeler { get; set; }
        public DbSet<MakaleTip> MakaleTipleri { get; set; }
        public DbSet<Etiket> Etiketler { get; set; }
        public DbSet<Ipucu> Ipuclari { get; set; }
        public DbSet<IsIlani> Ilanlar { get; set; }

        public DatabaseContextClass()
            : base("DbConnection3", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Makale>().
              HasMany(m=> m.Etiketler).
              WithMany(e => e.Makaleler).
              Map(
               m =>
               {
                   m.MapLeftKey("MakaleId");
                   m.MapRightKey("EtiketId");
                   m.ToTable("MakaleEtiket");
               });

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public static DatabaseContextClass Create()
        {
            return new DatabaseContextClass();
        }
    }
}
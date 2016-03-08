namespace AspNetMVC_CSharpTurk.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Duyuru",
                c => new
                    {
                        DuyuruId = c.Int(nullable: false, identity: true),
                        Onay = c.Boolean(nullable: false),
                        Kontrol = c.Boolean(nullable: false),
                        DuyuruTarih = c.DateTime(nullable: false),
                        DuyuruResimURL = c.String(),
                        DuyuruBaslik = c.String(nullable: false, maxLength: 100),
                        DuyuruGovde = c.String(nullable: false),
                        DuyuruOzet = c.String(nullable: false, maxLength: 200),
                        Kategori_KategoriId = c.Int(),
                        Yazar_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.DuyuruId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .ForeignKey("dbo.AspNetUsers", t => t.Yazar_Id)
                .Index(t => t.Kategori_KategoriId)
                .Index(t => t.Yazar_Id);
            
            CreateTable(
                "dbo.Kategori",
                c => new
                    {
                        KategoriId = c.Int(nullable: false, identity: true),
                        KategoriAdi = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.KategoriId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Ad = c.String(),
                        Soyad = c.String(),
                        WebSite = c.String(),
                        ResimURL = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.GorselDers",
                c => new
                    {
                        GorselDersId = c.Int(nullable: false, identity: true),
                        GorselDersBaslik = c.String(nullable: false),
                        GorselDersGovde = c.String(nullable: false),
                        GorselDersTarih = c.DateTime(nullable: false),
                        GorselDersResimURL = c.String(),
                        GorselDersDownloadURL = c.String(),
                        Kategori_KategoriId = c.Int(),
                        Yazar_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GorselDersId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .ForeignKey("dbo.AspNetUsers", t => t.Yazar_Id)
                .Index(t => t.Kategori_KategoriId)
                .Index(t => t.Yazar_Id);
            
            CreateTable(
                "dbo.Haber",
                c => new
                    {
                        HaberId = c.Int(nullable: false, identity: true),
                        HaberBaslik = c.String(nullable: false),
                        HaberOzet = c.String(nullable: false),
                        HaberGovde = c.String(nullable: false),
                        HaberTarih = c.DateTime(nullable: false),
                        Kategori_KategoriId = c.Int(),
                        Yazar_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.HaberId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .ForeignKey("dbo.AspNetUsers", t => t.Yazar_Id)
                .Index(t => t.Kategori_KategoriId)
                .Index(t => t.Yazar_Id);
            
            CreateTable(
                "dbo.HataBildir",
                c => new
                    {
                        HataId = c.Int(nullable: false, identity: true),
                        BildirilmeTarihi = c.DateTime(nullable: false),
                        Kontrol = c.Boolean(nullable: false),
                        Onay = c.Boolean(nullable: false),
                        Email = c.String(nullable: false),
                        HataMesaji = c.String(nullable: false, maxLength: 1000),
                        Kategori_KategoriId = c.Int(),
                    })
                .PrimaryKey(t => t.HataId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .Index(t => t.Kategori_KategoriId);
            
            CreateTable(
                "dbo.IlanIletisim",
                c => new
                    {
                        IlanId = c.Int(nullable: false, identity: true),
                        IlgiliKisi = c.String(nullable: false),
                        FirmaAdi = c.String(nullable: false),
                        EMail = c.String(nullable: false),
                        Telefon = c.String(nullable: false),
                        Il = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.IlanId)
                .ForeignKey("dbo.Ilan", t => t.IlanId)
                .Index(t => t.IlanId);
            
            CreateTable(
                "dbo.Ilan",
                c => new
                    {
                        IlanId = c.Int(nullable: false, identity: true),
                        IlanBaslik = c.String(nullable: false),
                        IlanGovde = c.String(nullable: false),
                        BasvuruSekli = c.String(nullable: false),
                        IlanTarih = c.DateTime(nullable: false),
                        Kategori_KategoriId = c.Int(),
                    })
                .PrimaryKey(t => t.IlanId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .Index(t => t.Kategori_KategoriId);
            
            CreateTable(
                "dbo.Indirme",
                c => new
                    {
                        IndirmeId = c.Int(nullable: false, identity: true),
                        IndirmeBaslik = c.String(nullable: false),
                        IndirmeOzet = c.String(nullable: false),
                        IndirmeGovde = c.String(nullable: false),
                        IndirmeTarih = c.DateTime(nullable: false),
                        IndirmeURL = c.String(nullable: false),
                        IndirmeResimURL = c.String(),
                        Kategori_KategoriId = c.Int(),
                    })
                .PrimaryKey(t => t.IndirmeId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .Index(t => t.Kategori_KategoriId);
            
            CreateTable(
                "dbo.Ipucu",
                c => new
                    {
                        IpucuId = c.Int(nullable: false, identity: true),
                        IpucuTarih = c.DateTime(nullable: false),
                        IpucuBaslik = c.String(nullable: false),
                        IpucuGovde = c.String(nullable: false),
                        Kategori_KategoriId = c.Int(),
                        Yazar_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IpucuId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .ForeignKey("dbo.AspNetUsers", t => t.Yazar_Id)
                .Index(t => t.Kategori_KategoriId)
                .Index(t => t.Yazar_Id);
            
            CreateTable(
                "dbo.Makale",
                c => new
                    {
                        MakaleId = c.Int(nullable: false, identity: true),
                        MakaleResimURL = c.String(),
                        MakaleTarih = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        MakaleBaslik = c.String(nullable: false, maxLength: 100),
                        MakaleGovde = c.String(nullable: false),
                        MakaleOzet = c.String(nullable: false),
                        Kategori_KategoriId = c.Int(),
                        Yazar_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.MakaleId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .ForeignKey("dbo.AspNetUsers", t => t.Yazar_Id)
                .Index(t => t.Kategori_KategoriId)
                .Index(t => t.Yazar_Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.Yorum",
                c => new
                    {
                        YorumId = c.Int(nullable: false, identity: true),
                        YorumYazanIsim = c.String(),
                        Eposta = c.String(),
                        YorumGovde = c.String(),
                        YorumTarih = c.DateTime(nullable: false),
                        Onay = c.Boolean(nullable: false),
                        Kontrol = c.Boolean(nullable: false),
                        Kategori_KategoriId = c.Int(),
                        Makale_MakaleId = c.Int(),
                    })
                .PrimaryKey(t => t.YorumId)
                .ForeignKey("dbo.Kategori", t => t.Kategori_KategoriId)
                .ForeignKey("dbo.Makale", t => t.Makale_MakaleId)
                .Index(t => t.Kategori_KategoriId)
                .Index(t => t.Makale_MakaleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Yorum", "Makale_MakaleId", "dbo.Makale");
            DropForeignKey("dbo.Yorum", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Makale", "Yazar_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Makale", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.Ipucu", "Yazar_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Ipucu", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.Indirme", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.IlanIletisim", "IlanId", "dbo.Ilan");
            DropForeignKey("dbo.Ilan", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.HataBildir", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.Haber", "Yazar_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Haber", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.GorselDers", "Yazar_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GorselDers", "Kategori_KategoriId", "dbo.Kategori");
            DropForeignKey("dbo.Duyuru", "Yazar_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Duyuru", "Kategori_KategoriId", "dbo.Kategori");
            DropIndex("dbo.Yorum", new[] { "Makale_MakaleId" });
            DropIndex("dbo.Yorum", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Makale", new[] { "Yazar_Id" });
            DropIndex("dbo.Makale", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.Ipucu", new[] { "Yazar_Id" });
            DropIndex("dbo.Ipucu", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.Indirme", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.Ilan", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.IlanIletisim", new[] { "IlanId" });
            DropIndex("dbo.HataBildir", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.Haber", new[] { "Yazar_Id" });
            DropIndex("dbo.Haber", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.GorselDers", new[] { "Yazar_Id" });
            DropIndex("dbo.GorselDers", new[] { "Kategori_KategoriId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Duyuru", new[] { "Yazar_Id" });
            DropIndex("dbo.Duyuru", new[] { "Kategori_KategoriId" });
            DropTable("dbo.Yorum");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Makale");
            DropTable("dbo.Ipucu");
            DropTable("dbo.Indirme");
            DropTable("dbo.Ilan");
            DropTable("dbo.IlanIletisim");
            DropTable("dbo.HataBildir");
            DropTable("dbo.Haber");
            DropTable("dbo.GorselDers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Kategori");
            DropTable("dbo.Duyuru");
        }
    }
}

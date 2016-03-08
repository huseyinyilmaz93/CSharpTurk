using AspNetMVC_CSharpTurk.Models.MembershipModels;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    [Table("AspNetUsers")]
    public class Yazar : Kullanici
    {
        public override int AccessFailedCount
        {
            get
            {
                return base.AccessFailedCount;
            }
            set
            {
                base.AccessFailedCount = value;
            }
        }
        public override ICollection<IdentityUserClaim> Claims
        {
            get
            {
                return base.Claims;
            }
        }
        [Display(Name="E Mail")]
        public override string Email
        {
            get
            {
                return base.Email;
            }
            set
            {
                base.Email = value;
            }
        }
        public override bool EmailConfirmed
        {
            get
            {
                return base.EmailConfirmed;
            }
            set
            {
                base.EmailConfirmed = value;
            }
        }
        public override string Id
        {
            get
            {
                return base.Id;
            }
            set
            {
                base.Id = value;
            }
        }
        public override bool LockoutEnabled
        {
            get
            {
                return base.LockoutEnabled;
            }
            set
            {
                base.LockoutEnabled = value;
            }
        }
        public override DateTime? LockoutEndDateUtc
        {
            get
            {
                return base.LockoutEndDateUtc;
            }
            set
            {
                base.LockoutEndDateUtc = value;
            }
        }
        public override ICollection<IdentityUserLogin> Logins
        {
            get
            {
                return base.Logins;
            }
        }
        public override string PasswordHash
        {
            get
            {
                return base.PasswordHash;
            }
            set
            {
                base.PasswordHash = value;
            }
        }
        public override string PhoneNumber
        {
            get
            {
                return base.PhoneNumber;
            }
            set
            {
                base.PhoneNumber = value;
            }
        }
        public override bool PhoneNumberConfirmed
        {
            get
            {
                return base.PhoneNumberConfirmed;
            }
            set
            {
                base.PhoneNumberConfirmed = value;
            }
        }
        public override ICollection<IdentityUserRole> Roles
        {
            get
            {
                return base.Roles;
            }
        }
        public override string SecurityStamp
        {
            get
            {
                return base.SecurityStamp;
            }
            set
            {
                base.SecurityStamp = value;
            }
        }
        public override bool TwoFactorEnabled
        {
            get
            {
                return base.TwoFactorEnabled;
            }
            set
            {
                base.TwoFactorEnabled = value;
            }
        }
        [Display(Name="Kullanıcı Adı")]
        public override string UserName
        {
            get
            {
                return base.UserName;
            }
            set
            {
                base.UserName = value;
            }
        }
    }
}
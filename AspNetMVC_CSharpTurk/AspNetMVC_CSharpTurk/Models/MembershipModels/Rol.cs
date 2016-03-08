using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.MembershipModels
{
    public class Rol : IdentityRole
    {
        public string Description { get; set; }

        public Rol()
        {

        }

        public Rol(string roleName, string description)
            : base(roleName)
        {
            this.Description = description;
        }
    }
}
using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class IpucuPostModel
    {
        [Required]
        public string IpucuBaslik { get; set; }
        [Required]
        public string IpucuGovde { get; set; }
    }
    public class Ipucu : IpucuPostModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IpucuId { get; set; }
        [Required]
        public DateTime IpucuTarih { get; set; }
        public virtual Kullanici Yazar { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
}
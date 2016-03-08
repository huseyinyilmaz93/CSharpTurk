using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class GorselDers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GorselDersId { get; set; }
        [Required]
        public string GorselDersBaslik { get; set; }
        [Required]
        public string GorselDersGovde { get; set; }
        [Required]
        public DateTime GorselDersTarih { get; set; }
        public string GorselDersResimURL { get; set; }
        public string GorselDersDownloadURL { get; set; }

        public virtual Kullanici Yazar { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
}
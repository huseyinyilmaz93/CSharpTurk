using AspNetMVC_CSharpTurk.Models.MembershipModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class Haber
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        [Key]
        public int HaberId { get; set; }
        [Required]
        public string HaberBaslik { get; set; }
        [Required]
        public string HaberOzet { get; set; }
        [Required]
        public string HaberGovde { get; set; }
        [Required]
        public DateTime HaberTarih { get; set; }
        [Required]
        public string ResimURL { get; set; }
        [Required]
        public string Link { get; set; }
        public string HaberUrl { get; set; }

        public virtual Kategori Kategori { get; set; }
        public virtual Kullanici Yazar { get; set; }
    }
}
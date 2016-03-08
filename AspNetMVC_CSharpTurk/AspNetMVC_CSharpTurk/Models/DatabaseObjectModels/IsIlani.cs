using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class IsIlani
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IlanId { get; set; }
        [Required]
        public string IlanBaslik { get; set; }
        [Required]
        public string IlanGovde { get; set; }

        public string ResimURL { get; set; }
        public string IlanOzet { get; set; }
        public string Link { get; set; }
        public string IlanUrl { get; set; }
        [Required]
        public DateTime IlanTarih { get; set; }

        public virtual Yazar Yazar { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
}
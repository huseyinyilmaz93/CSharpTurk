using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class Kategori
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int KategoriId { get; set; }
        [Required]
        public string KategoriAdi { get; set; }


    }
}

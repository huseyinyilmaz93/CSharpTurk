using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class Etiket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EtiketId { get; set; }
        public string EtiketAdi { get; set; }
        public string EtiketRadi { get; set; }

        public Etiket()
        {
            Makaleler = new HashSet<Makale>();
        }

        public virtual ICollection<Makale> Makaleler { get; set; }
    }
}
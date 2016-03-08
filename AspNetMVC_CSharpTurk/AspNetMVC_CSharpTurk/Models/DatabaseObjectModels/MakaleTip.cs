using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.DatabaseObjectModels
{
    public class MakaleTip
    {
        public int MakaleTipId { get; set; }
        public string MakaleTipAdi { get; set; }
        public string MakaleTipAciklama { get; set; }

        public virtual ICollection<Makale> Makaleler { get; set; }
    }
}
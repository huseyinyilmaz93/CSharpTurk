using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Haber
{
    public class HaberAPI
    {
        public static void KontrolEt()
        {
            DBYaz obj = new DBYaz();
            obj.HaberleriAlVeYazdir();
        }
    }
}
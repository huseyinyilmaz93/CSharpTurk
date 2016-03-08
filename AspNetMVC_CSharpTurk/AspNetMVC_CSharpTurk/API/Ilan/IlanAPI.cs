using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Ilan
{
    public class IlanAPI
    {
        public static void KontrolEt()
        {
            DBYaz obj = new DBYaz();
            obj.IlanleriAlVeYazdir();
        }

    }
}
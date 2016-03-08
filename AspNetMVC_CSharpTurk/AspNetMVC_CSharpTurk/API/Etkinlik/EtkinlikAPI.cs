using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspNetMVC_CSharpTurk.API.Etkinlik
{
    public class EtkinlikAPI
    {
        public static void KontrolEt()
        {
            DBYaz obj = new DBYaz();
            obj.HaberleriAlVeYazdir();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AspNetMVC_CSharpTurk.Models.MailModels
{
    public class MailGonderme
    {
        private static MailAddress gonderici = new MailAddress("csturk2016@gmail.com", "CSharpTürk Admin");
        private static string fromPassword = "csharpturk2016";
        private static string MesajBasligi = "CSharpTürk.Net Şifre Sıfırlama İsteği";
        private static string MesajGovde;

        public static void MailGonder(string yazarId,MailAddress alici)
        {
            MesajGovde = string.Concat("<h2> MERHABA, ", alici.DisplayName, "</h2><br />",
                "<p>Sizden e-mail sıfırlama isteği aldık. Bu <a href='http://www.csharpturk.net/Hesap/SifremiUnuttum/" + yazarId +"'>linke</a> tıklayarak,",
                " şifrenizi yenileyebilirsiniz. </p>",
                "<p>Uyarı, 3 saat içinde link zaman aşımına uğrayacaktır. Şifrenizi bu mail geldikten sonra 3 saat içinde değiştirmelisiniz!</p>",
                "<p>Bu değişiklik sizden gelmiyorsa lütfen bildirin </p><br />",
                "<h3>CShapTürk ekibi</h3>");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(gonderici.Address, fromPassword),
                Timeout = 20000
            };
            using (var message = new MailMessage(gonderici, alici)
            {
                IsBodyHtml = true,
                Subject = MesajBasligi,
                Body = MesajGovde
            })
            {
                smtp.Send(message);
            }

        }
    }
}
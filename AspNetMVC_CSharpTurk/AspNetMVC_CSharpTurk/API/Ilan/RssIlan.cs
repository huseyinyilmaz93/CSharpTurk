using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Xml.Linq;

namespace AspNetMVC_CSharpTurk.API.Ilan
{
    public class RssIlan
    {
        List<string> tags = new List<string>();

        public RssIlan()
        {
            tags.Add("asp.net");
            tags.Add("asp.net-mvc");
            tags.Add("asp.net-mvc-3");
            tags.Add("c#");
            tags.Add("web-api");
            tags.Add("mvc");
            tags.Add("unit-testing");
            tags.Add("ruby-on-rails");
        }

        private static string URL = "http://careers.stackoverflow.com/jobs/feed?location=istanbul&range=20&distanceUnits=Km";

        public IEnumerable<AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.IsIlani> GetRssFeed()
        {

            XDocument feedXml = XDocument.Load(URL);

            List<AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.IsIlani> isIlanlari =
                new List<AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.IsIlani>();
            bool flag = false;
            var items = feedXml.Descendants("item");
            foreach (var item in items)
            {
                var ilanTags = item.Descendants("category");
                foreach (var haberTag in ilanTags)
                {
                    string s = haberTag.Value;
                    flag = TagControl(s);
                    if (flag) break;
                }
                if (!flag) continue;
                AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.IsIlani isIlani =
                    new AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.IsIlani()
                    {
                        IlanTarih = (DateTime)item.Element("pubDate"),
                        Link = (string)item.Element("link"),
                        IlanBaslik = (string)item.Element("title"),
                        IlanOzet = (string)item.Element("description"),
                    };
                isIlani.IlanGovde = GetContent(isIlani.Link);
                isIlani.ResimURL = GetImage(isIlani.Link);
                isIlanlari.Add(isIlani);
                flag = false;
            }
            return isIlanlari;
        }

        private string GetContent(string link)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(IcerikGetir(link));
            return  document.DocumentNode.SelectSingleNode("//div[@class='jobdetail']").InnerHtml;

        }

        private string GetImage(string link)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(IcerikGetir(link));
            var imagesURL = document.DocumentNode.SelectNodes("//div[@class='company-page']//img/@src");
            if (imagesURL != null) return imagesURL.First().Attributes["src"].Value;
            // Eğer resim yoksa işe ait o zaman şirket logosunu alalım. 
            var logoUrl = document.DocumentNode.SelectNodes("//div[@id='companylogo']//img/@src");
            if(logoUrl != null) return logoUrl.First().Attributes["src"].Value;
            logoUrl = document.DocumentNode.SelectNodes("//div[@class='detail-company-block']//img/@src");
            return logoUrl.First().Attributes["src"].Value;
            
        }

        private bool TagControl(string specTag)
        {
            var a = from tag in tags
                    where tag == specTag
                    select tag;

            return a.Count() > 0;
        }

        private string IcerikGetir(string urlAddress)
        {
            Uri url = new Uri(urlAddress);
            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;

            string html = client.DownloadString(url);
            return html;
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using AspNetMVC_CSharpTurk.Models.DatabaseObjectModels;
using HtmlAgilityPack;

namespace AspNetMVC_CSharpTurk.API.Haber
{
    public class RssHaber
    {
        List<string> tags = new List<string>();

        public RssHaber()
        {
            tags.Add("Azure");
            tags.Add("Imagine Cup");
            tags.Add("Satya Nadella");
            tags.Add("DigiGirlz");
            tags.Add("MSP");
            tags.Add("Student Partner");
            tags.Add("Bulut");
        }

        private static string URL = "http://blog.microsoft.com.tr/?feed=rss2";

        public IEnumerable<AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Haber> GetRssFeed()
        {

            XDocument feedXml = XDocument.Load(URL);
            
            List<AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Haber> haberler = 
                new List<AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Haber>();
            bool flag = false;
            var items = feedXml.Descendants("item");
            foreach (var item in items)
            {
                var haberTags = item.Descendants("category");
                foreach (var haberTag in haberTags)
                {
                    string s = haberTag.Value;
                    flag = TagControl(s);
                    if (flag) break;
                }
                if (!flag) continue;
                AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Haber haber = 
                    new AspNetMVC_CSharpTurk.Models.DatabaseObjectModels.Haber()
                {
                    HaberTarih = (DateTime)item.Element("pubDate"),
                    Link = (string)item.Element("link"),
                    HaberBaslik = (string)item.Element("title"),
                    HaberOzet = (string)item.Element("description"),
                    HaberGovde = (string)item.Element("{http://purl.org/rss/1.0/modules/content/}encoded"),
                };
                haber.ResimURL = GetImage(haber.HaberGovde);
                haberler.Add(haber);
                flag = false;
            }
            return haberler;
        }

        public bool TagControl(string specTag)
        {
            var a = from tag in tags
                    where tag == specTag
                    select tag;

            return a.Count() > 0;
        }

        public string GetImage(string content)
        {
            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(content);
            var imagesURL = document.DocumentNode.SelectNodes("//img/@src");
            return imagesURL.First().Attributes["src"].Value;
        }

    }
}
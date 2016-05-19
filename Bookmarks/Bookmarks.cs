using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using System.Text.RegularExpressions;

namespace Bookmarks
{
    public class Bookmarks
    {
        private string url { get; set; }
        public string htmlString { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public string UrlImage { get; set; }




        public Bookmarks(string _url)
        {
            url = _url;
        }


        public bool UrlBuild()
        {
            try
            {
                var webcli = new WebClient();
                htmlString = webcli.DownloadString(url);
                BuildBookmarks();
                
                return true;

            }
            catch (Exception)
            {

                return false;
            }


        }

        private void BuildBookmarks()
        {

            var doc = new HtmlDocument();
            doc.LoadHtml(htmlString);
            var list = doc.DocumentNode.SelectNodes("//meta");
            IEnumerable<HtmlNode> metas = doc.DocumentNode.Descendants("meta").Where(x => x.Attributes.Contains("property"));

            //facebook
            Title = Title ?? FindAttr(metas, "property", "og:title");
            Description = Description ?? FindAttr(metas, "property", "og:description");
            UrlImage = UrlImage ?? FindAttr(metas, "property", "og:image");

            //twitter
            Title = Title ?? FindAttr(metas, "property", "twitter:title");
            Description = Description ?? FindAttr(metas, "property", "twitter:description");
            UrlImage = UrlImage ?? FindAttr(metas, "property", "twitter:image:src");

            Title = Title ?? Regex.Match(htmlString, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>",RegexOptions.IgnoreCase).Groups["Title"].Value;
            UrlImage = UrlImage ?? doc.DocumentNode.SelectSingleNode("//img/@src").InnerText;

        }

        public string FindAttr(IEnumerable<HtmlNode> metas, string strMetaAttr, string strMetaValue)
        {
            string str = null;
            foreach (var meta in metas)
            {
                if (meta.Attributes[strMetaAttr].Value == strMetaValue)
                {
                    str = meta.Attributes["content"].Value;
                }
            }

            return str;
        }



    }
}

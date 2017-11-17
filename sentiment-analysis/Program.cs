using System;
using System.Net;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Config.Site;
using sentimentanalysis.Core.Site.Entity;

namespace sentimentanalysis
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            AbstractSiteConfig siteConfig = new CoindeskConfig();
            Core.Site.WebClient webClient = new Core.Site.WebClient();
            WebPage webPage = webClient.GetPageFrom(siteConfig.BaseUrl);

            if (HttpStatusCode.OK == webPage.StatusCode)
            {
                HtmlParser parser = new HtmlParser(webPage);
                var titles = parser.GetElements(siteConfig.TitleCssSelector);

                foreach (var title in titles)
                {
                    Console.WriteLine(title.TextContent);
                }
            }

        }
    }
}

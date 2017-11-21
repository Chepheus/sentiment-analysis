﻿using System;
using AngleSharp.Dom;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Config.Site;
using sentimentanalysis.Core.Site.Entity;
using sentimentanalysis.Core.Site.Iterator;
using sentimentanalysis.Core.Site.Generator;

namespace sentimentanalysis
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            AbstractSiteConfig siteConfig = new CoindeskConfig();
            WebPagesIterator webPagesIterator = new WebPagesIterator(
                new UrlGenerator(siteConfig)
            );

            var i = 0;
            foreach (WebPage webPage in webPagesIterator)
            {
                HtmlParser htmlParser = new HtmlParser(webPage);
                IHtmlCollection<IElement> titles = htmlParser.GetElements(siteConfig.TitleCssSelector);
                foreach (var title in titles)
                {
                    Console.WriteLine(title.TextContent);
                }
                i++;

                if (3 == i) break;
            }
        }
    }
}

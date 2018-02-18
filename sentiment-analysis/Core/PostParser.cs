using System;
using System.Net;
using AngleSharp.Dom;
using sentimentanalysis.Core;
using sentimentanalysis.Config;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Core.Site.Entity;
using sentimentanalysis.Core.Site.Iterator;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core
{
    public class PostParser
    {
        protected PostService postService;
        protected WebPagesIterator webPagesIterator;
        protected ToLemmasConverter toLemmaConverter;
        protected CoreConfig config;

        public PostParser(PostService postService, 
                          WebPagesIterator webPagesIterator,
                          ToLemmasConverter toLemmaConverter,
                          CoreConfig config)
        {
            this.postService = postService;
            this.webPagesIterator = webPagesIterator;
            this.toLemmaConverter = toLemmaConverter;
            this.config = config;
        }

        public void Parse()
        {
			foreach (WebPage webPage in webPagesIterator)
			{
                try
                {
                    DateTime lastParsedTime = handleSavingWebPageData(webPage);
                    if (isNeedToInterrupt(lastParsedTime)) break;
                }
                catch(WebException e)
                {
                    Console.WriteLine(e.Message);
                }
			}
        }

        public void Parse(int start, int finish)
		{
			foreach (WebPage webPage in webPagesIterator)
			{
                try
                {
                    DateTime lastParsedTime = handleSavingWebPageData(webPage);

                    if (isNeedToInterrupt(lastParsedTime) || start == finish) break;
                }
                catch(WebException e)
                {
                    Console.WriteLine(e.Message);
                }
                start++;
			}
		}

        public void Parse(DateTime endTime)
        {
            foreach (WebPage webPage in webPagesIterator)
            {
                try
                {
                    DateTime lastParsedTime = handleSavingWebPageData(webPage);
                    if (isNeedToInterrupt(lastParsedTime, endTime)) break;
				}
				catch (WebException e)
				{
					Console.WriteLine(e.Message);
				}
            }
        }

        private void insertData(IHtmlCollection<IElement> titles,
                                  IHtmlCollection<IElement> times, 
                                IHtmlCollection<IElement> hrefs)
        {
            Console.WriteLine(hrefs.Length + "; " + titles.Length);
			for (int i = 0, l = titles.Length; i < l; i++)
			{
                string timeString = times[i].GetAttribute("datetime");
                string title = titles[i].TextContent;
                string href = hrefs[i].GetAttribute("href");

                if (0 == timeString.Length || 0 == title.Length || 0 == href.Length) continue;

                DateTime time = new TimeParser(timeString).GetDateTime();
                string lemmatizedTitle = toLemmaConverter.ToLemma(title);

                postService.Insert(new Post(lemmatizedTitle, href, time, config));
			}
        }

        private IHtmlCollection<IElement> getElements(WebPage webPage, string selector)
        {
            HtmlParser htmlParser = new HtmlParser(webPage);
            return htmlParser.GetElements(selector);
        }

        private DateTime handleSavingWebPageData(WebPage webPage)
        {
			IHtmlCollection<IElement> titles =
					getElements(webPage, config.SiteConfig.TitleCssSelector);

			IHtmlCollection<IElement> times =
				getElements(webPage, config.SiteConfig.TimeCssSelector);

            IHtmlCollection<IElement> hrefs = getElements(webPage, config.SiteConfig.HrefCssSelector);

            insertData(titles, times, hrefs);

			string lastPostTime = times[times.Length - 1].GetAttribute("datetime");
			return new TimeParser(lastPostTime).GetDateTime();
        }

        private bool isNeedToInterrupt(DateTime lastParsedTime)
        {
            DateTime startOf2k17 = config.TimeConfig.StartOf2k17;
            return DateTime.Compare(lastParsedTime, startOf2k17) < 1;
        }

        private bool isNeedToInterrupt(DateTime lastParsedTime, DateTime untilTime)
        {
            return DateTime.Compare(lastParsedTime, untilTime) < 1;
        }
    }
}

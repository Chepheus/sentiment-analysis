using System;
using AngleSharp.Dom;
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
        protected CoreConfig config;

        public PostParser(PostService postService, 
                          WebPagesIterator webPagesIterator, 
                          CoreConfig config)
        {
            this.postService = postService;
            this.webPagesIterator = webPagesIterator;
            this.config = config;
        }

        public void Parse()
        {
			foreach (WebPage webPage in webPagesIterator)
			{
                if (handleSavingWebPageData(webPage)) break;
			}
        }

        public void Parse(int start, int finish)
		{
			foreach (WebPage webPage in webPagesIterator)
			{
                if (handleSavingWebPageData(webPage)) break;

                if (start == finish) break;
                start++;
			}
		}

        private void insertData(IHtmlCollection<IElement> titles, 
                                  IHtmlCollection<IElement> times)
        {
			for (int i = 0, l = titles.Length; i < l; i++)
			{
				DateTime time = new TimeParser(times[i].GetAttribute("datetime")).GetDateTime();
				postService.Insert(new Post(titles[i], time, config));
			}
        }

        private IHtmlCollection<IElement> getElements(WebPage webPage, string selector)
        {
            HtmlParser htmlParser = new HtmlParser(webPage);
            return htmlParser.GetElements(selector);
        }

        private bool handleSavingWebPageData(WebPage webPage)
        {
			IHtmlCollection<IElement> titles =
					getElements(webPage, config.SiteConfig.TitleCssSelector);

			IHtmlCollection<IElement> times =
				getElements(webPage, config.SiteConfig.TimeCssSelector);

            insertData(titles, times);

            return isNeedToInterrupt(times);
        }

        private bool isNeedToInterrupt(IHtmlCollection<IElement> times)
        {
			string lastPostTime = times[times.Length - 1].GetAttribute("datetime");
			DateTime lastTime = new TimeParser(lastPostTime).GetDateTime();

			int result = DateTime.Compare(lastTime, config.TimeConfig.StartOf2k17);

            return result < 1;
        }
    }
}

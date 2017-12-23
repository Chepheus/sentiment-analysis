using System;
using System.Collections;
using sentimentanalysis.Core.Site.Entity;
using sentimentanalysis.Core.Site.Generator;

namespace sentimentanalysis.Core.Site.Iterator
{
    public class WebPagesIterator: IEnumerable
    {
        protected UrlGenerator urlGenerator;

        protected WebClient webClient;

        public WebPagesIterator(UrlGenerator urlGenerator, WebClient webClient)
        {
            this.urlGenerator = urlGenerator;
            this.webClient = webClient;
        }

        public IEnumerator GetEnumerator()
        {
            int pageNum = 1;
            WebPage webPage = null;
            do
            {
                Console.WriteLine("Url: " + urlGenerator.GenerateNextPageUrl(pageNum));
                webPage = webClient.GetPageFrom(urlGenerator.GenerateNextPageUrl(pageNum));
                pageNum++;

                yield return webPage;

            } while (System.Net.HttpStatusCode.OK == webPage.StatusCode);
        }
    }
}

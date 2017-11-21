using System;
using System.Net;
using System.Collections;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Core.Site.Entity;
using sentimentanalysis.Core.Site.Generator;

namespace sentimentanalysis.Core.Site.Iterator
{
    public class WebPagesIterator: IEnumerable
    {
        protected UrlGenerator urlGenerator;

        public WebPagesIterator(UrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
        }

        public IEnumerator GetEnumerator()
        {
            int pageNum = 1;
            WebPage webPage = null;
            do
            {
                WebClient webClient = new WebClient();
                Console.WriteLine("Url: " + urlGenerator.GenerateUrl(pageNum));
                webPage = webClient.GetPageFrom(urlGenerator.GenerateUrl(pageNum));
                pageNum++;

                yield return webPage;

            } while (HttpStatusCode.OK == webPage.StatusCode);
        }
    }
}

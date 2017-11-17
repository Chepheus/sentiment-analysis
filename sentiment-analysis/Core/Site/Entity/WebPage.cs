using System.Net;

namespace sentimentanalysis.Core.Site.Entity
{
    public class WebPage
    {
        private string _pageContent;

        private HttpStatusCode _statusCode;

        public string PageContent
        {
            get { return _pageContent; }
        }

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        public WebPage(string pageContent, HttpStatusCode statusCode)
        {
            _pageContent = pageContent;
            _statusCode = statusCode;
        }
    }
}

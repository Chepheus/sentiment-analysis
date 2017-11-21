using System.IO;
using System.Net;
using sentimentanalysis.Core.Site.Entity;

namespace sentimentanalysis.Core.Site
{
    public class WebClient
    {
        public WebPage GetPageFrom(string pageUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(pageUrl);
			request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            return getWebPage(response);
        }

        private WebPage getWebPage(HttpWebResponse response)
        {
			using (Stream stream = response.GetResponseStream())
			{
				StreamReader reader = new StreamReader(stream);

				string content = reader.ReadToEnd();
				HttpStatusCode statusCode = response.StatusCode;

				return new WebPage(content, statusCode);
			}
        }
    }
}

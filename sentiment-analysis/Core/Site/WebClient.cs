using System.IO;
using System.Net;
using sentimentanalysis.Config.Site;
using sentimentanalysis.Core.Site.Entity;

namespace sentimentanalysis.Core.Site
{
    public class WebClient
    {
        public WebPage GetPageFrom(AbstractSiteConfig siteConfig)
        {
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(siteConfig.BaseUrl);
			request.Method = "GET";
			var response = (HttpWebResponse)request.GetResponse();

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

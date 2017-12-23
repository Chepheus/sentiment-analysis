using System;
namespace sentimentanalysis.Config.Site
{
    public class ApiConfig
    {
        public string BaseUrl
        {
            get { return "https://api.coindesk.com";  }
        }

        public string DateRangeUrlTemplate
        {
            get
            { 
                return BaseUrl + "/v1/bpi/historical/close.json?start={0}&end={1}"; 
            }
        }
    }
}

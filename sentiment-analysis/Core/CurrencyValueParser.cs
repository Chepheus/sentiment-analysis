using System;
using System.Globalization;
using Newtonsoft.Json.Linq;
using sentimentanalysis.Config;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Core.Site.Entity;
using sentimentanalysis.Core.Site.Generator;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core
{
    public class CurrencyValueParser
    {
        protected WebClient webClient;

        protected UrlGenerator urlGenerator;

        protected CoreConfig config;

        protected CurrencyValueService currencyValueService;

        public CurrencyValueParser(WebClient webClient,
                                   UrlGenerator urlGenerator,
                                   CurrencyValueService currencyValueService,
                                   CoreConfig config)
        {
            this.webClient = webClient;
            this.urlGenerator = urlGenerator;
            this.config = config;
            this.currencyValueService = currencyValueService;
        }

        public void Parse()
        {
            string apiUrl = urlGenerator.GenerateApiUrl();
            parseAndInsertWebPageData(apiUrl);
        }

        public void Parse(DateTime startTime, DateTime endTime)
        {
            string apiUrl = urlGenerator.GenerateApiUrl(startTime, endTime);
            parseAndInsertWebPageData(apiUrl);
        }

        private void parseAndInsertWebPageData(string apiUrl)
        {
            Console.WriteLine(apiUrl);
			WebPage webPage = webClient.GetPageFrom(apiUrl);
			if (System.Net.HttpStatusCode.OK == webPage.StatusCode)
			{
				JObject parsedJson = JObject.Parse(webPage.PageContent);
				foreach (var val in parsedJson["bpi"].Children())
				{
					string[] dateAndValue = val.ToString().Split(':');

					float value = getValue(dateAndValue[1]);
					DateTime time = getTime(dateAndValue[0]);

					currencyValueService.Insert(new CurrencyValue(value, time, config));
				}
			}
        }

        private float getValue(string valueString)
        {
            return Convert.ToSingle(valueString, new CultureInfo("en-US"));
        }

        private DateTime getTime(string timeString)
        {
            return new TimeParser(timeString.Substring(1, timeString.Length - 2))
                .ExtractDateTime();
        }
    }
}

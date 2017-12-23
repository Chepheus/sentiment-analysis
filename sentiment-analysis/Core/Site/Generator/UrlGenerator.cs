using System;
using sentimentanalysis.Config;
using sentimentanalysis.Config.Common;

namespace sentimentanalysis.Core.Site.Generator
{
    public class UrlGenerator
    {
        protected CoreConfig config;

        public UrlGenerator(CoreConfig config)
        {
            this.config = config;
        }

        public string GenerateNextPageUrl(int pageNum)
        {
            string template = config.SiteConfig.NextPageUrlTemplate;
            return String.Format(template, pageNum);
        }

		public string GenerateApiUrl()
		{
            TimeConfig timeConfig = config.TimeConfig;

			string template = config.ApiConfig.DateRangeUrlTemplate;
            string startDate = timeConfig.StartOf2k17.ToString(timeConfig.ApiTimeFormat);
            string endDate = timeConfig.EndOf2k17.ToString(timeConfig.ApiTimeFormat);

			return String.Format(template, startDate, endDate);
		}

        public string GenerateApiUrl(DateTime startDate, DateTime endDate)
        {
            string template = config.ApiConfig.DateRangeUrlTemplate;
            string formattingStartDate = startDate.ToString(config.TimeConfig.ApiTimeFormat);
            string formattingEndDate = endDate.ToString(config.TimeConfig.ApiTimeFormat);

            return String.Format(template, formattingStartDate, formattingEndDate);
        }
    }
}

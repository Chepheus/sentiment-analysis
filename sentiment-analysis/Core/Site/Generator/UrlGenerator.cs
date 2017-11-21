using System;
using sentimentanalysis.Config.Site;

namespace sentimentanalysis.Core.Site.Generator
{
    public class UrlGenerator
    {
        protected AbstractSiteConfig siteConfig;

        public UrlGenerator(AbstractSiteConfig siteConfig)
        {
            this.siteConfig = siteConfig;
        }

        public string GenerateUrl(int pageNum)
        {
            return String.Format(siteConfig.NextPageUrlTemplate, pageNum);
        }
    }
}

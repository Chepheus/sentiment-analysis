namespace sentimentanalysis.Config.Site
{
    public abstract class AbstractSiteConfig
    {
        protected string baseUrl;

        protected string titleCssSelector;

        protected string nextPagePostfix;

        public string BaseUrl
        {
            get { return baseUrl; }
        }

        public string TitleCssSelector
        {
            get { return titleCssSelector; }
        }

        public string NextPageUrlTemplate
        {
            get { return BaseUrl + nextPagePostfix; }
        }

        public AbstractSiteConfig()
        {
            baseUrl = "";
            nextPagePostfix = "";
            titleCssSelector = "";
        }
    }
}

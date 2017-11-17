namespace sentimentanalysis.Config.Site
{
    public class CoindeskConfig: AbstractSiteConfig
    {
        public CoindeskConfig()
        {
            baseUrl = "https://www.coindesk.com/";
            titleCssSelector = "h3";
            nextPagePostfix = "page/" + pageNumberTemplate + "/";
        }
    }
}

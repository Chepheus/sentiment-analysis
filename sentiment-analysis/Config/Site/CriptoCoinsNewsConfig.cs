namespace sentimentanalysis.Config.Site
{
    public class CriptoCoinsNewsConfig: AbstractSiteConfig
    {
        public CriptoCoinsNewsConfig()
        {
            baseUrl = "https://www.cryptocoinsnews.com/news/";
            titleCssSelector = "h3.entry-title > a";
            nextPagePostfix = "page/" + pageNumberTemplate + "/";
        }
    }
}

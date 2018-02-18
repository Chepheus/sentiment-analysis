namespace sentimentanalysis.Config.Site
{
    public class CoindeskConfig: AbstractSiteConfig
    {
        public CoindeskConfig()
        {
            baseUrl = "https://www.coindesk.com/";
            titleCssSelector = "div:not(#coindesk_follow_us_widget-2) > h3";
            timeCssSelector = "p.timeauthor > time";
            hrefCssSelector = "div a.fade";
            nextPagePostfix = "page/{0}/";
        }
    }
}

using AngleSharp.Dom;
using AngleSharp.Dom.Html;
using sentimentanalysis.Core.Site.Entity;

namespace sentimentanalysis.Core.Site
{
    public class HtmlParser
    {
        protected AngleSharp.Parser.Html.HtmlParser parser;

        protected IHtmlDocument document;

        public HtmlParser(WebPage webPage)
        {
            parser = new AngleSharp.Parser.Html.HtmlParser();
            document = parser.Parse(webPage.PageContent);
        }

        public IHtmlCollection<IElement> GetElements(string cssSelector)
        {
            return document.QuerySelectorAll(cssSelector);
        }
    }
}

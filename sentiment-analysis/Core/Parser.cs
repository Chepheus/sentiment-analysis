using sentimentanalysis.Config;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Core.Site.Iterator;
using sentimentanalysis.Core.Site.Generator;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;


namespace sentimentanalysis.Core
{
    public class Parser
    {
        protected PostService postService;
        protected CurrencyValueService currencyValueService;
        protected WebPagesIterator webPagesIterator;
        protected UrlGenerator urlGenerator;
        protected WebClient webClient;
        protected CoreConfig config;

        public Parser(PostService postService,
                      CurrencyValueService currencyValueService,
                      CoreConfig config)
        {
            this.postService = postService;
            this.currencyValueService = currencyValueService;
            this.urlGenerator = new UrlGenerator(config);
            this.webClient = new WebClient();
            this.webPagesIterator = new WebPagesIterator(urlGenerator, webClient);
            this.config = config;
        }

        public void Parse()
        {
			Post post = postService.SelectLastRecord(config);
			PostParser postParser = new PostParser(postService, webPagesIterator, config);

			if (null != post)
			{
				postParser.Parse(post.DateTime);
			}
			else
			{
				postParser.Parse();
			}

			CurrencyValue currencyValue = currencyValueService.SelectLastRecord(config);
			CurrencyValueParser currencyValueParser =
				new CurrencyValueParser(webClient, urlGenerator, currencyValueService, config);

			if (null != currencyValue)
			{
				currencyValueParser.Parse(
					currencyValue.DateTime, config.TimeConfig.EndOf2k17
				);
			}
			else
			{
				currencyValueParser.Parse();
			}
        }
    }
}

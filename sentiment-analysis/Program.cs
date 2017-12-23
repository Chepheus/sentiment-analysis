using MySql.Data.MySqlClient;
using sentimentanalysis.Core;
using sentimentanalysis.Config;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Core.Site.Iterator;
using sentimentanalysis.Core.Site.Generator;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            CoreConfig config = new CoreConfig();
            WebClient webClient = new WebClient();

            MySqlConnection connection = 
                new MySqlConnection(config.MySqlConfig.ConnectionString);
                            
            connection.Open();

            DataSetter setter = new DataSetter(connection);
            DataFetcher fetcher = new DataFetcher(connection);

			UrlGenerator urlGenerator = new UrlGenerator(config);
            WebPagesIterator webPagesIterator = new WebPagesIterator(
                urlGenerator, webClient
            );
            PostService postService = new PostService(setter, fetcher);
            CurrencyValueService currencyValueService = new CurrencyValueService(setter, fetcher);

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

            connection.Close();
        }
    }
}

using System;
using MySql.Data.MySqlClient;
using sentimentanalysis.Core;
using sentimentanalysis.Config;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;
using sentimentanalysis.Core.Database.Service.Common;
using sentimentanalysis.Core.Analysis.Analizators;

namespace sentimentanalysis
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            CoreConfig config = new CoreConfig();

            MySqlConnection connection = 
                new MySqlConnection(config.MySqlConfig.ConnectionString);
                            
            connection.Open();

            DataSetter setter = new DataSetter(connection);
            DataFetcher fetcher = new DataFetcher(connection);

            PostService postService = new PostService(setter, fetcher);
            CurrencyValueService currencyValueService = new CurrencyValueService(setter, fetcher);
            PostExtremumService postExtremumService = new PostExtremumService(setter, fetcher);
            WordsService wordService = new WordsService(setter, fetcher);
            PostWordService postWordService = new PostWordService(setter, fetcher);
            WordsFilterService wordsFilterService = new WordsFilterService(
                setter, fetcher, wordService, postWordService, postExtremumService
            );

			/********** I **********/
			//Parser parser = new Parser(postService, currencyValueService, config);
			//parser.Parse();
			/********** I **********/

			/********** II **********/
			//AnalizatorFactory analizatorFactory = 
			//    new AnalizatorFactory(postService, currencyValueService, postExtremumService);

            //analizatorFactory.GetAnalizator(Extremum.SHARP_GROWTH).Analize(config);
			/********** II **********/

			/********** III **********/
			wordsFilterService.InsertFilteredWords();
			/********** III **********/
			
            connection.Close();
        }
    }
}

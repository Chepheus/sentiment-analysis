using System;
using MySql.Data.MySqlClient;
using sentimentanalysis.Core;
using sentimentanalysis.Config;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;
using sentimentanalysis.Core.Database.Service.Common;
using sentimentanalysis.Core.Analysis.Analizators;
using sentimentanalysis.Core.Analysis.Service;
using sentimentanalysis.Core.Analysis;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using System.Linq;

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
            ConfigService configService = new ConfigService(setter, fetcher);

			SentimentCoeficientCalculator calcuator =
				new SentimentCoeficientCalculator(wordService, postExtremumService);
            InputPostAnalisator inputPostAnalizator = 
                new InputPostAnalisator(new ToLemmasConverter(), calcuator);

			/********** I **********/
			//Parser parser = new Parser(postService, currencyValueService, config);
			//LastTimeParseLogger ltParser = new LastTimeParseLogger(parser, configService, postService, config);
			//ltParser.Parse();
			//parser.Parse();
			/********** I **********/

			/********** II **********/
			// Analyze extremums 
			//AnalizatorFactory analizatorFactory = 
			//    new AnalizatorFactory(postService, currencyValueService, postExtremumService);

			//analizatorFactory.GetAnalizator(Extremum.FROM_FALL_TO_GROWTH).Analize(config);
			//analizatorFactory.GetAnalizator(Extremum.SHARP_GROWTH).Analize(config);

			//analizatorFactory.GetAnalizator(Extremum.FROM_GROWTH_TO_FALL).Analize(config);
			//analizatorFactory.GetAnalizator(Extremum.SHARP_FALL).Analize(config);
			/********** II **********/

			/********** III **********/
			// Only for extremums
			//wordsFilterService.InsertFilteredWords();
			/********** III **********/

			/********** IV **********/
			ConfigEntity lastPostTime = configService.Get(ConfigEntity.LAST_POST_TIME);
            TimeParser timeParser = new TimeParser(lastPostTime.Value);
            List<Post> posts = postService.GetPostsSinceDate(timeParser.GetDateTime(), config);

            Dictionary<Post, float> estimatedPosts = inputPostAnalizator.GetPosts(posts);

            IEnumerable<KeyValuePair<Post, float>> orderedEstimatedPosts = estimatedPosts.Where(pair => Math.Abs(pair.Value) >= 0.02);
            TelegramSender telegramSender = new TelegramSender(config);
            List<Task<Message>> taskList = telegramSender.SendEstimatedPosts(orderedEstimatedPosts);

            Task.WaitAll(taskList.ToArray());
			/********** IV **********/

			connection.Close();
        }
    }
}

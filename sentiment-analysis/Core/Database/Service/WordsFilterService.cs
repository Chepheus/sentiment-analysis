using System;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis.Core.Database.Service
{
    public class WordsFilterService: DatabseService
    {
        protected WordsService wordService;
        protected PostWordService postWordService;
        protected PostExtremumService postExtremumService;

        protected override string getTableName()
        {
            return "words_filter";
        }
        
        public WordsFilterService(DataSetter setter, 
                                  DataFetcher fetcher, 
                                  WordsService wordService, 
                                  PostWordService postWordService, 
                                  PostExtremumService postExtremumService)
            : base(setter, fetcher)
        {
            this.wordService = wordService;
            this.postWordService = postWordService;
            this.postExtremumService = postExtremumService;
        }

        public bool Isset(string word)
		{
			string select = "WHERE word = \"{0}\"";
            string preparedSql = String.Format(select, word);
			string[] fields = new string[] { "word_id", "word" };

			List<Dictionary<string, object>> result =
				fetcher.Fetch(_select(preparedSql), fields);

			return result.Count > 0;
		}

        public void InsertFilteredWords()
        {
			var allPosts = postExtremumService.SelectAllRelatedPosts();

			foreach (var post in allPosts)
			{
				if (postWordService.Isset(Convert.ToInt32(post["post_id"]))) continue;

				string[] wordsList = post["title"].ToString().Split(' ');
				foreach (string wordString in wordsList)
				{
                    int tmpInt;
					if (0 == wordString.Trim().Length
						|| Isset(wordString)
						|| Int32.TryParse(wordString, out tmpInt))
					{
						continue;
					}

					int extremumId = Convert.ToInt32(post["extremum_id"]);
					bool isPositive = extremumId == Extremum.FROM_FALL_TO_GROWTH
									  || extremumId == Extremum.SHARP_GROWTH;

					Word word = new Word(wordString, isPositive);
					wordService.Insert(word);

					var postWord = new PostWord(
						Convert.ToInt32(post["post_id"]),
						Convert.ToInt32(wordService.GetLastInsertedId())
					);
					postWordService.Insert(postWord);
				}
			}
        }
    }
}
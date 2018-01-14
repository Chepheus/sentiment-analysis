using System;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis.Core.Database.Service
{
    public class WordsService : DatabseService
    {
        protected override string getTableName()
        {
            return "words";
        }
        
        public WordsService(DataSetter setter, DataFetcher fetcher)
            : base(setter, fetcher) {}

        public void Insert(Word word)
		{
			try
			{
				string query = "(word, is_positive) VALUES(\"{0}\", {1})";
				string preparedSql = String.Format(query, word.Value, word.IsPositive);

				setter.Insert(_insert(preparedSql));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public bool Isset(Word word)
		{
			string select = "WHERE word = \"{0}\" AND is_positive = {1}";
			string preparedSql =
                String.Format(select, word.Value, word.IsPositive);
			string[] fields = { "word", "is_positive" };

			List<Dictionary<string, object>> result =
				fetcher.Fetch(_select(preparedSql), fields);

			return result.Count > 0;
		}
    }
}

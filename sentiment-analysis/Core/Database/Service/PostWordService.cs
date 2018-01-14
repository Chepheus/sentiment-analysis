using System;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis.Core.Database.Service
{
    public class PostWordService: DatabseService
    {
        protected override string getTableName()
        {
            return "post_words";
        }
        
        public PostWordService(DataSetter setter, DataFetcher fetcher)
            : base(setter, fetcher){}

        public void Insert(PostWord postWord)
		{
			try
			{
                if (Isset(postWord)) return;

				string query = "(post_id, word_id) VALUES({0}, {1})";
				string preparedSql = String.Format(
					query,
					postWord.PostId,
                    postWord.WordId
				);

				setter.Insert(_insert(preparedSql));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

        public bool Isset(PostWord postWord)
		{
			string select = "WHERE post_id = {0} AND word_id = {1}";
			string preparedSql =
                String.Format(select, postWord.PostId, postWord.WordId);

            return _isset(preparedSql);
		}

        public bool Isset(int postId)
		{
			string select = "WHERE post_id = {0}";
            string preparedSql = String.Format(select, postId);

            return _isset(preparedSql);
		}

        private bool _isset(string preparedSql)
        {
			string[] fields = { "post_id", "word_id" };

			List<Dictionary<string, object>> result =
				fetcher.Fetch(_select(preparedSql), fields);

			return result.Count > 0;
        }
    }
}

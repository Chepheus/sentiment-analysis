using System;
using sentimentanalysis.Config;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis.Core.Database.Service
{
    public class PostService: DatabseService
    {
        protected override string getTableName()
        {
            return "posts";
        }
        
        public PostService(DataSetter setter, DataFetcher fetcher)
            : base(setter, fetcher){}

        public void Insert(Post post)
        {
            try
            {
                if (Isset(post)) return;

                string query = "(title, post_date) VALUES(\"{0}\", \"{1}\")";
                string preparedSql = String.Format(query, post.Title, post.Time);

                Console.WriteLine(_insert(preparedSql));
                setter.Insert(_insert(preparedSql));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public bool Isset(Post post)
        {
            string select = "WHERE title = \"{0}\" AND post_date = \"{1}\"";
			string preparedSql = String.Format(select, post.Title, post.Time);
            string[] fields = new string[] {"post_id", "title", "post_date" };

            List<Dictionary<string, object>> result = 
                fetcher.Fetch(_select(preparedSql), fields);
            return result.Count > 0;
        }

        public Post SelectLastRecord(CoreConfig config)
        {
            string select = "ORDER BY post_date DESC LIMIT 1";
            string[] fields = new string[] { "post_id", "title", "post_date" };

            List<Dictionary<string, object>> result = fetcher.Fetch(_select(select), fields);

            if (result.Count > 0)
            {
                Dictionary<string, object> dbPostObject = result[0];

                return new Post(
                    dbPostObject["title"].ToString(), 
                    DateTime.Parse(dbPostObject["post_date"].ToString()),
                    config
                );
            }

            return null;
        }
    }
}

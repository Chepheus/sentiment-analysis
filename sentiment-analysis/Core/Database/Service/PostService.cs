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

                string query = "(title, href, post_date) VALUES(\"{0}\", \"{1}\", \"{2}\")";
                string preparedSql = String.Format(query, post.Title, post.Href, post.Time);

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
            string[] fields = new string[] {"post_id", "title", "href", "post_date" };

            List<Dictionary<string, object>> result = 
                fetcher.Fetch(_select(preparedSql), fields);
            
            return result.Count > 0;
        }

        public Post SelectLastRecord(CoreConfig config)
        {
            string select = "ORDER BY post_date DESC LIMIT 1";
            string[] fields = new string[] { "post_id", "title", "href", "post_date" };

            List<Dictionary<string, object>> result = fetcher.Fetch(_select(select), fields);

            if (result.Count > 0)
            {
                Dictionary<string, object> dbPostObject = result[0];

                return new Post(
                    dbPostObject["title"].ToString(),
                    dbPostObject["href"].ToString(),
                    DateTime.Parse(dbPostObject["post_date"].ToString()),
                    config
                );
            }

            return null;
        }

        public List<Post> GetPostsByDate(DateTime time, CoreConfig config)
        {
            TimeSpan dayScatter = new TimeSpan(config.TimeConfig.DayScatter, 0, 0, 0);
            string select = String.Format(
                "WHERE post_date BETWEEN \"{0}\" AND \"{1}\"", 
                time.Subtract(dayScatter).ToString(config.TimeConfig.TimeFormat), 
                time.Add(dayScatter).ToString(config.TimeConfig.TimeFormat)
            );

            return _fetchPosts(select, config);
        }

		public List<Post> GetPostsSinceDate(DateTime time, CoreConfig config)
		{
			string select = String.Format(
				"WHERE post_date > \"{0}\"",
				time.ToString(config.TimeConfig.TimeFormat)
			);

            return _fetchPosts(select, config);
		}

        private List<Post> _fetchPosts(string select, CoreConfig config)
        {
			string[] fields = new string[] { "post_id", "title", "href", "post_date" };

			Console.WriteLine(_select(select));
			List<Dictionary<string, object>> result = fetcher.Fetch(_select(select), fields);
			List<Post> posts = new List<Post>();

			foreach (Dictionary<string, object> rowPost in result)
			{
				posts.Add(
					new Post(
						Convert.ToInt32(rowPost["post_id"]),
						rowPost["title"].ToString(),
                        rowPost["href"].ToString(),
						DateTime.Parse(rowPost["post_date"].ToString()),
						config
					)
				);
			}

			return posts;
        }
    }
}

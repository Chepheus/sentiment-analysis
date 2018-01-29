using System;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis.Core.Database.Service
{
	public class PostExtremumService : DatabseService
	{
        protected override string getTableName()
		{
			return "post_extremum";
		}

		public PostExtremumService(DataSetter setter, DataFetcher fetcher)
			: base(setter, fetcher) { }

        public void Insert(PostExtremum postExtremum)
		{
			try
			{
				if (Isset(postExtremum)) return;

                string query = "(post_id, value_id, extremum_id) VALUES({0}, {1}, {2})";
                string preparedSql = String.Format(
                    query,
                    postExtremum.PostId, 
                    postExtremum.CurrencyValueId, 
                    postExtremum.ExtremumId
                );

				setter.Insert(_insert(preparedSql));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

		public bool Isset(PostExtremum postExtremum)
		{
			string select = "WHERE post_id = {0} AND extremum_id = {1}";
            string preparedSql = 
                String.Format(select, postExtremum.PostId, postExtremum.ExtremumId);
			string[] fields = { "post_extremum_id", "post_id", "extremum_id" };

			List<Dictionary<string, object>> result =
				fetcher.Fetch(_select(preparedSql), fields);
            
			return result.Count > 0;
		}

		public List<Dictionary<string, object>> SelectAllRelatedPosts()
		{
			string select = "INNER JOIN posts ON posts.post_id = post_extremum.post_id";
			string[] fields = new string[] { "post_id", "title", "value_id", "extremum_id" };

			return fetcher.Fetch(_select(select), fields);
		}

        public int Count(bool isPositive)
        {
            string select = "SELECT COUNT(*) FROM {0} WHERE extremum_id = {1} OR extremum_id = {2}";
            string preparedSql;

            if (isPositive)
            {
                preparedSql = String.Format(
                    select,
                    getTableName(),
                    Extremum.FROM_FALL_TO_GROWTH,
                    Extremum.SHARP_GROWTH
                );
            }
            else 
            {
				preparedSql = String.Format(
                    select,
					getTableName(),
                    Extremum.FROM_GROWTH_TO_FALL,
                    Extremum.SHARP_FALL
                );
            }

            return Convert.ToInt32(fetcher.Scalar(preparedSql));
        }
	}
}

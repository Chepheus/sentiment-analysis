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
	}
}

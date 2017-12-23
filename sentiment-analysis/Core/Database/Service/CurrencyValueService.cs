using System;
using sentimentanalysis.Config;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service.Common;


namespace sentimentanalysis.Core.Database.Service
{
	public class CurrencyValueService: DatabseService
    {
        protected override string getTableName()
        {
            return "currency_value";
        }

        public CurrencyValueService(DataSetter setter, DataFetcher fetcher)
            : base(setter, fetcher) { }
        
        public void Insert(CurrencyValue currencyValue)
		{
			try
			{
                if (Isset(currencyValue)) return;

                string query = "(value, close_date) VALUES(\"{0}\", \"{1}\")";
                string preparedSql = String.Format(query, currencyValue.Value, currencyValue.Time);

                setter.Insert(_insert(preparedSql));
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
		}

        public bool Isset(CurrencyValue currencyValue)
		{
            string select = "WHERE value = \"{0}\" AND close_date = \"{1}\"";
            string preparedSql = String.Format(select, currencyValue.Value, currencyValue.Time);
            string[] fields = new string[] { "value_id", "value", "close_date" };

            List<Dictionary<string, object>> result = fetcher.Fetch(_select(preparedSql), fields);

            return result.Count > 0;
		}

        public CurrencyValue SelectLastRecord(CoreConfig config)
		{
			string select = "ORDER BY close_date DESC LIMIT 1";
			string[] fields = new string[] { "value_id", "value", "close_date" };

			List<Dictionary<string, object>> result = fetcher.Fetch(_select(select), fields);

			if (result.Count > 0)
			{
				Dictionary<string, object> dbPostObject = result[0];

                return new CurrencyValue(
                    Convert.ToSingle(dbPostObject["value"].ToString()),
					DateTime.Parse(dbPostObject["close_date"].ToString()),
					config
				);
			}

			return null;
		}
    }
}

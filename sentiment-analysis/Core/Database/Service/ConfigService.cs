using System;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis.Core.Database.Service
{    
    public class ConfigService: DatabseService
    {
		protected override string getTableName()
		{
			return "config";
		}

        public ConfigService(DataSetter setter, DataFetcher fetcher)
            : base(setter, fetcher){}

        public void Set(string title, string value)
        {
            ConfigEntity existConfigEntity = Get(title);
            if (null != existConfigEntity)
            {
                Update(new ConfigEntity(existConfigEntity.Title, value));
            }
            
            string sql = String.Format(
                "(title, value) VALUES(\"{0}\", \"{1}\")", title, value
            );

            setter.Insert(_insert(sql));
        }

        public ConfigEntity Get(string title)
        {
            string sql = String.Format("WHERE title = \"{0}\"", title);
            return _get(sql);
        }

        public ConfigEntity Get(int configId)
        {
            string sql = String.Format("WHERE config_id = {0}", configId);
            return _get(sql);
        }

        public void Update(ConfigEntity configEntityNew)
        {
            string sql = "UPDATE {0} SET value = {1} WHERE title = {2}";
            string preparedSql = String.Format(sql, getTableName(), 
                                               configEntityNew.Value, 
                                               configEntityNew.Title);
        }
        
        private ConfigEntity _get(string sql)
        {
			string[] fields = { "title", "value" };
			List<Dictionary<string, object>> results =
				fetcher.Fetch(_select(sql), fields);

			if (results.Count > 0)
			{
				return new ConfigEntity(
					Convert.ToString(results[0]["title"]),
					Convert.ToString(results[0]["value"])
				);
			}

			return null;
        }
    }
}

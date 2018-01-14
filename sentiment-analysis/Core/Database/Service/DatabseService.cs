using System;
using sentimentanalysis.Core.Database.Service.Common;

namespace sentimentanalysis.Core.Database.Service
{
    public abstract class DatabseService
    {
        protected DataSetter setter;

        protected DataFetcher fetcher;

        public DatabseService(DataSetter setter, DataFetcher fetcher)
        {
            this.fetcher = fetcher;
            this.setter = setter;
        }

		public long GetLastInsertedId()
		{
			return setter.GetLastInsertedId();
		}

        protected abstract string getTableName();

        protected string _insert(string inserBody)
        {
            if (0 == getTableName().Length)
            {
                throw new Exception("Table name can't be empty");
            }

            return string.Format("INSERT INTO {0} {1}", getTableName(), inserBody);
        }

        protected string _select(string whereClouse)
        {
            if (0 == getTableName().Length)
			{
				throw new Exception("Table name can't be empty");
			}

            return string.Format("SELECT * FROM {0} {1}", getTableName(), whereClouse);
        }
    }
}

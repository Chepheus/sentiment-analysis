using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace sentimentanalysis.Core.Database.Service.Common
{
    public class DataFetcher: AbstractDatabaseHandler
    {
        public DataFetcher(MySqlConnection connection):base(connection){}

        public List<Dictionary<string, object>> Fetch(string sql, string[] fields)
		{
			List<Dictionary<string, object>> result =
				new List<Dictionary<string, object>>();

			command.CommandText = sql;
			MySqlDataReader reader = command.ExecuteReader();

			while (reader.Read())
			{
				result.Add(getLine(reader, fields));
			}

			reader.Close();

			return result;
		}

        public object Scalar(string sql)
        {
			command.CommandText = sql;
            return command.ExecuteScalar();
        }

		private Dictionary<string, object> getLine(MySqlDataReader reader, string[] fields)
		{
			Dictionary<string, object> line = new Dictionary<string, object>();
			foreach (string field in fields)
			{
				line.Add(field, reader[field]);
			}

			return line;
		}
    }
}

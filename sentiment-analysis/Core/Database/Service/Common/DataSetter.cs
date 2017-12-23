using MySql.Data.MySqlClient;

namespace sentimentanalysis.Core.Database.Service.Common
{
    public class DataSetter: AbstractDatabaseHandler
    {
        public DataSetter(MySqlConnection connection): base(connection){}

        public void Insert(string sql)
        {
			command.CommandText = sql;
			command.ExecuteNonQuery();
        }
    }
}

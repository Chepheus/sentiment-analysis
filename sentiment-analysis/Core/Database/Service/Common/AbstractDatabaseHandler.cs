using MySql.Data.MySqlClient;

namespace sentimentanalysis.Core.Database.Service.Common
{
    public class AbstractDatabaseHandler
    {
		protected MySqlConnection connection;

		protected MySqlCommand command;

		public AbstractDatabaseHandler(MySqlConnection connection)
		{
			this.connection = connection;
			command = new MySqlCommand();
			command.Connection = connection;
		}
    }
}

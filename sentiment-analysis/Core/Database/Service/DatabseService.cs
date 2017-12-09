using MySql.Data.MySqlClient;
using sentimentanalysis.Core.Database.Entity;

namespace sentimentanalysis.Core.Database.Service
{
    public abstract class DatabseService
    {
        protected MySqlConnection connection;

        protected MySqlCommand command;

        protected bool isOpened = false;

        public DatabseService(MySqlConnection connection)
        {
            this.connection = connection;
            command = new MySqlCommand();
            command.Connection = connection;
        }
    }
}

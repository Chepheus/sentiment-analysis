using System;
namespace sentimentanalysis.Config.Database
{
    public class MySqlConfig
    {
        public string Database => "sentiment_analysis";
        public string DataStructure => "172.23.0.7";
        public string UserId => "zamant";
        public string Password => "zamant";

        public string ConnectionString => 
                       "Database=" + this.Database + ";" +
                       "Data Source=" + this.DataStructure + ";" +
                       "User Id=" + this.UserId + ";" +
                       "Password=" + this.Password;
    }
}

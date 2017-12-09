using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;

namespace sentimentanalysis.Core.Database.Service
{
    public class PostService: DatabseService
    {
        public PostService(MySqlConnection connection)
            : base(connection){}

        public void Insert(Post post)
        {
            try
            {
                List<Dictionary<string, object>> dbPost = Select(post);
                if (dbPost.Count > 0) return;

                string query = "INSERT INTO posts (title, post_date) VALUES(\"{0}\", \"{1}\")";
                string preparedSql = String.Format(query, post.Title, post.Time);

                command.CommandText = preparedSql;
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public List<Dictionary<string, object>> Select(Post post)
        {
            List<Dictionary<string, object>> result =
                new List<Dictionary<string, object>>();

			string select = "SELECT * FROM posts WHERE title = \"{0}\" AND post_date = \"{1}\"";
			string preparedSql = String.Format(select, post.Title, post.Time);

            command.CommandText = preparedSql;
            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                Dictionary<string, object> line = new Dictionary<string, object>()
                {
                    { "post_id", reader["post_id"] },
                    { "title", reader["title"] },
                    { "post_date", reader["post_date"] }
                };

                result.Add(line);
            }

            reader.Close();

            return result;
        }
    }
}

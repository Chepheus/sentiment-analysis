using System;
using AngleSharp.Dom;
using MySql.Data.MySqlClient;
using sentimentanalysis.Core;
using sentimentanalysis.Config;
using sentimentanalysis.Core.Site;
using sentimentanalysis.Config.Site;
using System.Text.RegularExpressions;
using sentimentanalysis.Config.Database;
using sentimentanalysis.Core.Site.Entity;
using sentimentanalysis.Core.Site.Iterator;
using sentimentanalysis.Core.Site.Generator;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            CoreConfig config = new CoreConfig();

            MySqlConnection connection = 
                new MySqlConnection(config.MySqlConfig.ConnectionString);
            
            connection.Open();

            WebPagesIterator webPagesIterator = new WebPagesIterator(
                new UrlGenerator(config.SiteConfig)
            );
            PostService postService = new PostService(connection);

            PostParser postParser = new PostParser(postService, webPagesIterator, config);
            postParser.Parse();

            connection.Close();
        }
    }
}

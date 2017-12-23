using System;
using AngleSharp.Dom;
using sentimentanalysis.Config;
using System.Text.RegularExpressions;

namespace sentimentanalysis.Core.Database.Entity
{
    public class Post
    {
        protected string title;

        protected DateTime time;

        protected CoreConfig config;

        public string Title
        {
            get 
            {
                return Regex.Replace(
                    title.Trim(),
                    config.TextConfig.TextFilter,
                    " "
                );
            }
        }

        public string Time
        {
            get { return time.ToString(config.TimeConfig.TimeFormat); }
        }

        public DateTime DateTime
        {
            get { return time; }
        }

        public Post(string title, DateTime time, CoreConfig config)
        {
            this.title = title;
            this.time = time;
            this.config = config;
        }
    }
}

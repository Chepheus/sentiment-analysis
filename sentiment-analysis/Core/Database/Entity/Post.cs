using System;
using AngleSharp.Dom;
using sentimentanalysis.Config;
using System.Text.RegularExpressions;

namespace sentimentanalysis.Core.Database.Entity
{
    public class Post
    {
        protected IElement title;

        protected DateTime time;

        protected CoreConfig config;

        public string Title
        {
            get 
            {
                return Regex.Replace(
                    title.TextContent.Trim(),
                    config.TextConfig.TextFilter,
                    " "
                );
            }
        }

        public string Time
        {
            get { return time.ToString(config.TimeConfig.TimeFormat); }
        }

        public Post(IElement title, DateTime time, CoreConfig config)
        {
            this.title = title;
            this.time = time;
            this.config = config;
        }
    }
}

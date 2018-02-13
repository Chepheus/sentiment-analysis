using System;
using sentimentanalysis.Config;
using System.Text.RegularExpressions;

namespace sentimentanalysis.Core.Database.Entity
{
    public class Post
    {
        protected int postId;

        protected string title;

        protected string href;

        protected DateTime time;

        protected CoreConfig config;

        public int PostId
        {
            get { return postId; }
        }

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

        public string Href
        {
            get { return href; }
        }

        public string Time
        {
            get { return time.ToString(config.TimeConfig.TimeFormat); }
        }

        public DateTime DateTime
        {
            get { return time; }
        }

        public Post(string title, string href, DateTime time, CoreConfig config)
        {
            init(title, href, time, config);
        }

        public Post(int postId, string title, string href, DateTime time, CoreConfig config)
        {
            init(title, href, time, config);
            this.postId = postId;
        }

        private void init(string title, string href, DateTime time, CoreConfig config)
        {
			this.title = title;
            this.href = href;
			this.time = time;
			this.config = config;
        }
    }
}

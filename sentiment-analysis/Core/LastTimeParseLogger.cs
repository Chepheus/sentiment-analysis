using System;
using sentimentanalysis.Config;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core
{
    public class LastTimeParseLogger
    {
        protected Parser parser;
        protected ConfigService configService;
        protected PostService postService;
        protected CoreConfig config;

        public LastTimeParseLogger(Parser parser,
                                   ConfigService configService,
                                   PostService postService, 
                                   CoreConfig config)
        {
            this.parser = parser;
            this.configService = configService;
            this.postService = postService;
            this.config = config;
        }

        public void Parse()
        {
            Post post = postService.SelectLastRecord(config);
            if (null != post)
            {
                configService.Set("last_post_time", post.Time);
            }
            else 
            {
                string time = DateTime.Now.ToString(config.TimeConfig.TimeFormat);
                configService.Set("last_post_time", time);
            }

            parser.Parse();
        }
    }
}

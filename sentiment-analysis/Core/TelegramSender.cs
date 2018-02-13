using System;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using sentimentanalysis.Config;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;

namespace sentimentanalysis.Core
{
    public class TelegramSender
    {
        protected CoreConfig config;
        protected TelegramBotClient botClient;

        public TelegramSender(CoreConfig config)
        {
            this.config = config;
            botClient = new TelegramBotClient(config.TelegramConfig.ApiKey);
        }

        public async Task<Message> Send(string message)
        {
            return await botClient.SendTextMessageAsync(chatId: config.TelegramConfig.MyUserId, 
                                                 text: message);
        }

        public List<Task<Message>> SendEstimatedPosts(
            IEnumerable<KeyValuePair<Post, float>> estimatedPosts
        )
        {
            List<Task<Message>> taskList = new List<Task<Message>>();

            foreach(var estimatedPost in estimatedPosts)
            {
				taskList.Add(
				    Send(
                        String.Format("{0}\nCoeficient: {1}",
									  estimatedPost.Key.Href,
									  estimatedPost.Value
									 )
					)
				);    
            }

            return taskList;
        }
    }
}

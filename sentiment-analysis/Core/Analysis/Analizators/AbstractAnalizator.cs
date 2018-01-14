using System;
using sentimentanalysis.Config;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core.Analysis.Analizators
{
    public abstract class AbstractAnalizator
    {
        protected PostService postService;
        protected CurrencyValueService currencyValueService;
        protected PostExtremumService postExtremumService;
        
        public AbstractAnalizator(PostService postService, 
                                  CurrencyValueService currencyValueService, 
                                  PostExtremumService postExtremumService)
        {
            this.postService = postService;
            this.currencyValueService = currencyValueService;
            this.postExtremumService = postExtremumService;
        }

		public void Analize(CoreConfig config)
		{
			List<Dictionary<string, object>> data = currencyValueService.SelectAll(config);

			bool isGrowed = true;
			int percentLimit = getPercentLimit(data);
			for (int i = 0, l = data.Count; i < l; i++)
			{
				float current_value = getValue(data[i]);
				float next_value = (l - 1 == i) ? getValue(data[i]) : getValue(data[i + 1]);

				float avg = (current_value + next_value) / 2f;
				bool isGrowing = (avg - current_value) > 0;
				int percent = (int)(Math.Abs((avg / current_value) - 1) * 100);

                if (graphicBehaviour(isGrowed, isGrowing) && percent >= 5)
				{
					CurrencyValue currencyValue = new CurrencyValue(
						Convert.ToInt32(data[i]["value_id"]),
						Convert.ToSingle(data[i]["value"]),
						DateTime.Parse(data[i]["close_date"].ToString()),
						config
					);

					List<Post> posts = postService.GetPostsByDate(currencyValue.DateTime, config);
					foreach (Post post in posts)
					{
						PostExtremum postExtremum = new PostExtremum(
							post,
                            new Extremum(getExtremumId()),
							currencyValue
						);

						postExtremumService.Insert(postExtremum);
					}
				}

				isGrowed = isGrowing;
			}
		}

        protected abstract bool graphicBehaviour(bool isGrowed, bool isGrowing);

        protected abstract int getExtremumId();

		protected int getPercentLimit(List<Dictionary<string, object>> data)
		{
			int summOfPercent = 0;
			for (int i = 0, l = data.Count; i < l; i++)
			{
				float current_value = getValue(data[i]);
				float next_value = (l - 1 == i) ? getValue(data[i]) : getValue(data[i + 1]);

				float avg = (current_value + next_value) / 2f;
				int percent = (int)(Math.Abs((avg / current_value) - 1) * 100);

				summOfPercent += percent;
			}

			return summOfPercent / data.Count;
		}

		protected float getValue(Dictionary<string, object> row)
		{
			return Convert.ToSingle(row["value"]);
		}
    }
}

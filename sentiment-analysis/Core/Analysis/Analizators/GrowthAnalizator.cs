using System;
using sentimentanalysis.Config;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core.Analysis.Analizators
{
    public class GrowthAnalizator: AbstractAnalizator
    {
        public GrowthAnalizator(PostService postService, CurrencyValueService currencyValueService, PostExtremumService postExtremumService)
            :base(postService, currencyValueService, postExtremumService){}

        public override void Analize(CoreConfig config)
        {
			List<Dictionary<string, object>> data = currencyValueService.SelectAll(config);

			bool is_growed = true;
			int percentLimit = getPercentLimit(data);
			for (int i = 0, l = data.Count; i < l; i++)
			{
				float current_value = getValue(data[i]);
				float next_value = (l - 1 == i) ? getValue(data[i]) : getValue(data[i + 1]);

				float avg = (current_value + next_value) / 2f;
				bool is_growing = (avg - current_value) > 0;
				int percent = (int)(Math.Abs((avg / current_value) - 1) * 100);

				bool is_down = is_growed && !is_growing;
				bool is_up = !is_growed && is_growing;
				if (is_up && percent >= 5)
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
                            new Extremum(Extremum.FROM_FALL_TO_GROWTH),
                            currencyValue
                        );

                        postExtremumService.Insert(postExtremum);
					}
				}

				is_growed = is_growing;
			}
        }
    }
}

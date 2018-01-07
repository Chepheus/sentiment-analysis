using System;
using sentimentanalysis.Config;
using System.Collections.Generic;
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

        public abstract void Analize(CoreConfig config);

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

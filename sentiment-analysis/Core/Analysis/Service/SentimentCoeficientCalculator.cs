using System;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core.Analysis.Service
{
    public class SentimentCoeficientCalculator
    {
        protected WordsService wordService;
        protected PostExtremumService postExtremumService;

		private int positivePostCount;
		private int negativePostCount;
        
        public SentimentCoeficientCalculator(WordsService wordService, PostExtremumService postExtremumService)
        {
            this.wordService = wordService;
            this.postExtremumService = postExtremumService;
        }

        public float GetCoeficient(string word, bool strategy)
		{
			int wordCount = wordService.Count(new Word(word, strategy));
			return (float)wordCount / (float)getPostCount(strategy);
		}

        protected int getPostCount(bool strategy)
		{
			return strategy
				? getPositivePostCount()
				: getNegativePostCount();
		}

        protected int getPositivePostCount()
		{
			if (0 == positivePostCount)
            {
				positivePostCount = postExtremumService.Count(true);
			}

            return positivePostCount;
		}

        protected int getNegativePostCount()
		{
			if (0 == negativePostCount)
			{
				negativePostCount = postExtremumService.Count(false);
			}

            return negativePostCount;
		}
    }
}

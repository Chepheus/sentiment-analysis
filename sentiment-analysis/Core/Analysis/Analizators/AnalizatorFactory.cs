using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core.Analysis.Analizators
{
	public class AnalizatorFactory
	{
        protected PostService postService;
        protected CurrencyValueService currencyValueService;
        protected PostExtremumService postExtremumService;

        public AnalizatorFactory(PostService postService, 
                                 CurrencyValueService currencyValueService, 
                                 PostExtremumService postExtremumService)
        {
            this.postService = postService;
            this.currencyValueService = currencyValueService;
            this.postExtremumService = postExtremumService;
        }
        
		public AbstractAnalizator GetAnalizator(int analizerType)
		{
			switch (analizerType)
			{
                case Extremum.FROM_FALL_TO_GROWTH:
                    return new GrowthAnalizator(
                        this.postService,
                        this.currencyValueService,
                        this.postExtremumService
                    );

                case Extremum.FROM_GROWTH_TO_FALL:
                    return new FallAnalizator(
						this.postService,
						this.currencyValueService,
						this.postExtremumService
                    );
                case Extremum.SHARP_GROWTH:
                    return new SharpGrowthAnalizator(
						this.postService,
						this.currencyValueService,
						this.postExtremumService
                    );
                case Extremum.SHARP_FALL:
                    return new SharpFallAnalizator(
						this.postService,
						this.currencyValueService,
						this.postExtremumService
                    );
			}

            return null;
		}
	}
}

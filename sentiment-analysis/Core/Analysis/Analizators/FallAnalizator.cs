using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Database.Service;

namespace sentimentanalysis.Core.Analysis.Analizators
{
    public class FallAnalizator: AbstractAnalizator
    {
		public FallAnalizator(PostService postService,
							  CurrencyValueService currencyValueService,
							  PostExtremumService postExtremumService)
        : base(postService, currencyValueService, postExtremumService) { }

        protected override bool graphicBehaviour(bool isGrowed, bool isGrowing)
        {
            return isGrowed && !isGrowing;
        }

        protected override int getExtremumId()
        {
            return Extremum.FROM_GROWTH_TO_FALL;
        }
    }
}

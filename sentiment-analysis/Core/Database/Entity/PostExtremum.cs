namespace sentimentanalysis.Core.Database.Entity
{
    public class PostExtremum
    {
        protected Post post;
        protected Extremum extremum;
        protected CurrencyValue currencyValue;

        public int PostId
        {
            get { return post.PostId; }
        }

        public int ExtremumId
        {
            get { return extremum.ExtremumId; }
        }

        public int CurrencyValueId
        {
            get { return currencyValue.ValueId; }
        }

        public PostExtremum(Post post, Extremum extremum, CurrencyValue currencyValue)
        {
            this.post = post;
            this.extremum = extremum;
            this.currencyValue = currencyValue;
        }
    }
}

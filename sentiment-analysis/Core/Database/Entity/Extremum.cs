namespace sentimentanalysis.Core.Database.Entity
{
    public class Extremum
    {
        public const int FROM_GROWTH_TO_FALL = 1;
        public const int FROM_FALL_TO_GROWTH = 2;
        public const int SHARP_RISE_IN_GROWTH = 3;
        public const int SHARP_RISE_IN_DROP = 4;

        protected int extremum_id;

        public int ExtremumId
        {
            get { return extremum_id; }
        }

        public Extremum(int extremum_id)
        {
            this.extremum_id = extremum_id;
        }
    }
}

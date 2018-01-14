namespace sentimentanalysis.Core.Database.Entity
{
    public class WordsFilter
    {
        protected string word;

        public string Word
        {
            get { return word; }
        }

        public WordsFilter(string word)
        {
            this.word = word;
        }
    }
}

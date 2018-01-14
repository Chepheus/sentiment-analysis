namespace sentimentanalysis.Core.Database.Entity
{
    public class Word
    {
        protected string word;
        protected bool isPositive;

        public string Value
        {
            get { return word;  }
        }

        public bool IsPositive
        {
            get { return isPositive; }
        }

        public Word(string word, bool isPositive)
        {
            this.word = word;
            this.isPositive = isPositive;
        }
    }
}

namespace sentimentanalysis.Core.Database.Entity
{
    public class PostWord
    {
        protected int postId;
        protected int wordId;

        public int PostId
        {
            get { return postId; }
        }

        public int WordId
        {
            get { return wordId; }
        }

        public PostWord(int postId, int wordId)
        {
            this.postId = postId;
            this.wordId = wordId;
        }
    }
}

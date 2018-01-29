namespace sentimentanalysis.Core.Database.Entity
{
    public class ConfigEntity
    {
        public const int LAST_POST_TIME = 1;

        protected string title;
        protected string value;

        public string Title
        {
            get { return title; }
        }

        public string Value
        {
            get { return value; }
        }

        public ConfigEntity(string title, string value)
        {
            this.title = title;
            this.value = value;
        }
    }
}

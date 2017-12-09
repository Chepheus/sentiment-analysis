using sentimentanalysis.Config.Site;
using sentimentanalysis.Config.Common;
using sentimentanalysis.Config.Database;

namespace sentimentanalysis.Config
{
    public class CoreConfig
    {
        public TextConfig TextConfig
        {
            get { return new TextConfig(); }
        }

        public TimeConfig TimeConfig
        {
            get { return new TimeConfig(); }
        }

        public MySqlConfig MySqlConfig
        {
            get { return new MySqlConfig(); }
        }

        public AbstractSiteConfig SiteConfig
        {
            get { return new CoindeskConfig(); }
        }
    }
}

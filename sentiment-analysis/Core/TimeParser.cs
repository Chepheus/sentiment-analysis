using System;
namespace sentimentanalysis.Core
{
    public class TimeParser
    {
        protected string timeString;

        public TimeParser(string timeString)
        {
            this.timeString = timeString;
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(timeString).ToUniversalTime();
        }
    }
}

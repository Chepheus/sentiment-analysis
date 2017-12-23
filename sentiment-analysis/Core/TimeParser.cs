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

        public DateTime ExtractDateTime()
        {
            string[] splitedTime = timeString.Split('-');

            int year = Convert.ToInt32(splitedTime[0]);
            int month = Convert.ToInt32(splitedTime[1]);
            int day = Convert.ToInt32(splitedTime[2]);

            return new DateTime(year, month, day);
        }
    }
}

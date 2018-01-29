using System;
namespace sentimentanalysis.Config.Common
{
    public class TimeConfig
    {
        public string TimeFormat
        {
            get { return "yyyy-MM-dd HH:mm:ss"; }
        }

        public string ApiTimeFormat
        {
            get { return "yyyy-MM-dd"; }
        }

        public DateTime StartOf2k17
        {
            get { return new DateTime(2017, 1, 1); }
        }

        public DateTime EndOfDate
        {
            get { return new DateTime(2100, 12, 31); }
        }

        public int DayScatter
        {
            get { return 1; }
        }
    }
}

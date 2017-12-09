using System;
namespace sentimentanalysis.Config.Common
{
    public class TimeConfig
    {
        public string TimeFormat
        {
            get { return "yyyy-MM-dd H:mm:ss"; }
        }

        public DateTime StartOf2k17
        {
            get { return new DateTime(2017, 1, 1); }
        }
    }
}

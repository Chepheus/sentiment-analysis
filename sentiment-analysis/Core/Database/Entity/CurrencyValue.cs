using System;
using sentimentanalysis.Config;

namespace sentimentanalysis.Core.Database.Entity
{
    public class CurrencyValue
    {
        protected DateTime time;
        protected float value;
        protected CoreConfig config;

        public float Value
        {
            get { return value; }
        }

		public string Time
		{
			get { return time.ToString(config.TimeConfig.TimeFormat); }
		}

        public DateTime DateTime
        {
            get { return time; }
        }

        public CurrencyValue(float value, DateTime closeDate, CoreConfig config)
        {
            this.value = value;
            this.time = closeDate;
            this.config = config;
        }
    }
}

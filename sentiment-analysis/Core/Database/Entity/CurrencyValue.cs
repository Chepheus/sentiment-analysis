using System;
using sentimentanalysis.Config;

namespace sentimentanalysis.Core.Database.Entity
{
    public class CurrencyValue
    {
        protected int valueId;
        protected DateTime time;
        protected float value;
        protected CoreConfig config;

        public int ValueId
        {
            get { return valueId; }
        }

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
            init(value, closeDate, config);
        }

		public CurrencyValue(int valueId, float value, DateTime closeDate, CoreConfig config)
		{
            init(value, closeDate, config);
            this.valueId = valueId;
		}

        private void init(float value, DateTime closeDate, CoreConfig config)
        {
			this.value = value;
			this.time = closeDate;
			this.config = config;
        }
    }
}

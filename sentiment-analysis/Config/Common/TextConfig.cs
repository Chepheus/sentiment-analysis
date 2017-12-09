namespace sentimentanalysis.Config.Common
{
    public class TextConfig
    {
		public string TextFilter
		{
			get { return @"[^0-9a-zA-Z\$]+"; }
		}
    }
}

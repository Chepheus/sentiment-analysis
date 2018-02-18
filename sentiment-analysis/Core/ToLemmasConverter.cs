using System;
namespace sentimentanalysis.Core
{
    public class ToLemmasConverter
    {
        public string ToLemma(string str)
        {
            for (int i = str.Length >= 4 ? 4 : str.Length; i > 0; i--)
            {
                string part = str.Substring(str.Length - i, i);

                switch(part)
                {
                    case "sses": return str.Replace(part, "ss");
                    case "ies": return str.Replace(part, "i");
                    case "ss": return str.Replace(part, "ss");
                    case "s": return str.Replace(part, "");
                }
            }

            return str;
        }
    }
}


namespace Kids.Utility.UtilExtension.NumberExtensions
{
    internal static class NumberArticulator
    {
        public static string ConvertNumberToWord(long numberVal)
        {
            string[] powers = new string[] { "thousand ", "million ", 
          "billion " };

            string[] ones = new string[] {"one", "two", "three", "four", 
          "five", "six", "seven", "eight", "nine", "ten",
          "eleven", "twelve", "thirteen", "fourteen", "fifteen",
          "sixteen", "seventeen", "eighteen", "nineteen"};

            string[] tens = new string[] {"twenty", "thirty", "forty", 
         "fifty", "sixty", "seventy", "eighty", "ninety"};

            string wordValue = "";

            if (numberVal == 0) return "zero";
            if (numberVal < 0)
            {
                wordValue = "negative ";
                numberVal = -numberVal;
            }

            long[] partStack = new long[] { 0, 0, 0, 0 };
            int partNdx = 0;

            while (numberVal > 0)
            {
                partStack[partNdx++] = numberVal % 1000;
                numberVal /= 1000;
            }

            for (int i = 3; i >= 0; i--)
            {
                long part = partStack[i];

                if (part >= 100)
                {
                    wordValue += ones[part / 100 - 1] + " hundred ";
                    part %= 100;
                }

                if (part >= 20)
                {
                    if ((part % 10) != 0) wordValue += tens[part / 10 - 2] +
                       " " + ones[part % 10 - 1] + " ";
                    else wordValue += tens[part / 10 - 2] + " ";
                }
                else if (part > 0) wordValue += ones[part - 1] + " ";

                if (part != 0 && i > 0) wordValue += powers[i - 1];
            }

            return wordValue.Trim();
        }
    }
}
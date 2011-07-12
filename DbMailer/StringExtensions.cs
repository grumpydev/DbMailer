namespace DbMailer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StringExtensions
    {
        public static void ParseInt(this string input, Action<int> outputDelegate)
        {
            int temp;
            
            if (int.TryParse(input, out temp))
            {
                outputDelegate.Invoke(temp);
            }
        }

        public static string AsCommaSeparated(this IEnumerable<string> input)
        {
            var inputStrings = input.ToArray();

            return inputStrings.Any() ? inputStrings.Aggregate((s1, s2) => s1 + "," + s2) : "None";
        }
    }
}
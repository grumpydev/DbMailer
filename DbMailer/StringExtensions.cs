namespace DbMailer
{
    using System;

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
    }
}
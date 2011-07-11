namespace DbMailer
{
    using System;
    using System.Linq;

    public class CommandLineParser
    {
        private readonly string[] basicArgs;

        private readonly Tuple<string, string>[] nameValueArgs;

        private readonly StringComparison stringComparison;

        public CommandLineParser(string[] args, StringComparison stringComparison = StringComparison.InvariantCultureIgnoreCase)
        {
            this.basicArgs = args.Where(a => !a.Contains(':')).ToArray();
            
            this.nameValueArgs = args.Where(a => a.Contains(':'))
                                     .Select(a =>
                                         {
                                             var split = a.Split(':');
                                             return new Tuple<string, string>(split[0], split[1]);
                                         }).ToArray();

            this.stringComparison = stringComparison;
        }

        public bool ArgumentExists (string argumentName)
        {
            return this.basicArgs.Any(s => String.Equals(s, argumentName, this.stringComparison));
        }

        public void ParseArgument(string argumentName, Action<string> setValueDelegate)
        {
            var argument =
                this.nameValueArgs.FirstOrDefault(a => String.Equals(a.Item1, argumentName, this.stringComparison));

            if (argument == null)
            {
                return;
            }

            setValueDelegate.Invoke(argument.Item2);
        }
    }
}
namespace DbMailer
{
    using System;

    class Program
    {
        static int Main(string[] args)
        {
            var settings = new Settings();
            var parser = new CommandLineParser(args);

            parser.ParseArgument("connectionString", v => settings.ConnectionString = v);
            parser.ParseArgument("sql", v => settings.Sql = v);
            parser.ParseArgument("from", v => settings.From = v);
            parser.ParseArgument("to", v => settings.To = v.Split(',', ';'));
            parser.ParseArgument("cc", v => settings.To = v.Split(',', ';'));
            parser.ParseArgument("bcc", v => settings.To = v.Split(',', ';'));
            parser.ParseArgument("subject", v => settings.Subject = v);
            parser.ParseArgument("body", v => settings.Body = v);
            parser.ParseArgument("smtpServer", v => settings.SmtpServer = v);
            parser.ParseArgument("smtpUsername", v => settings.SmtpUsername = v);
            parser.ParseArgument("smtpPassword", v => settings.SmtpPassword = v);
            parser.ParseArgument("smtpPort", v => v.ParseInt(i => settings.SmtpPort = i));
            settings.HtmlBody = parser.ArgumentExists("-html");

            if (!settings.IsValid)
            {
                ShowUsage();
                
                return -1;
            }

            var mailer = new CsvMailer(settings);

            return mailer.Process() ? 0 : -2;
        }

        private static void ShowUsage()
        {
            Console.WriteLine("Usage");
        }
    }
}

namespace DbMailer
{
    using System;

    class Program
    {
        static int Main(string[] args)
        {
            ShowHeader();

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
            parser.ParseArgument("executionTimeout", v => v.ParseInt(i => settings.ExecutionTimeout = i));
            settings.HtmlBody = parser.ArgumentExists("-html");

            if (!settings.IsValid)
            {
                ShowUsage(args);

                return -1;
            }

            Console.WriteLine("Executing using connfiguration:\n");
            Console.WriteLine(settings);

            var mailer = new CsvMailer(settings);

            return mailer.Process() ? 0 : -2;
        }

        private static void ShowHeader()
        {
            Console.WriteLine();
            Console.WriteLine("Database Mailer");
            Console.WriteLine();
            Console.WriteLine("A utility for executing SQL statements and emailing the results as a CSV.");
            Console.WriteLine();
        }

        private static void ShowUsage(string[] args)
        {
            Console.WriteLine("Usage: DbMailer.exe connectionString:<connectionString> sql:<sqlToExecute> from:<fromAddress> [<recipients>] [options]");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine("\texecutionTimeout:<seconds>\tSQL execution timeout");
            Console.WriteLine("\tto:<recipients>\t\t\tRecipients to send to");
            Console.WriteLine("\tcc:<ccRecipients>\t\tRecipients to cc");
            Console.WriteLine("\tbcc:<ccRecipients>\t\tRecipients to bcc");
            Console.WriteLine("\tsubject:<emailSubject>\t\tEmail subject");
            Console.WriteLine("\tbody:<emailBody>\t\tEmail body (see -html)");
            Console.WriteLine("\t-html\t\t\t\tBody specified is html");

            Console.WriteLine("\tsmtpServer:<server>\t\tSMTP server to use");
            Console.WriteLine("\tsmtpPort:<port>\t\t\tPort to use");
            Console.WriteLine("\tsmtpUsername:<username>\t\tUsername to use");
            Console.WriteLine("\tsmtpPassword:<password>\t\tPassword to use");

            Console.WriteLine();

            Console.WriteLine("Email addresses (to, cc, bcc) should take the form of comma separated lists and at least one must be specified.");

            Console.WriteLine();
        }
    }
}

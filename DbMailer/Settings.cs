namespace DbMailer
{
    using System.Collections.Generic;
    using System.Linq;

    public class Settings
    {
        public string ConnectionString { get; set; }

        public string Sql { get; set; }

        public string From { get; set; }

        public IEnumerable<string> To { get; set; }

        public IEnumerable<string> Cc { get; set; }

        public IEnumerable<string> Bcc { get; set; } 

        public string Subject { get; set; }

        public string Body { get; set; }

        public bool HtmlBody { get; set; }

        public string SmtpServer { get; set; }

        public int? SmtpPort { get; set; }

        public string SmtpUsername { get; set; }

        public string SmtpPassword { get; set; }

        public int ExecutionTimeout { get; set; }

        public Settings()
        {
            this.Subject = "DbMailer Email";
            this.Body = string.Empty;
            this.HtmlBody = false;

            this.To = new string[] { };
            this.Cc = new string[] { };
            this.Bcc = new string[] { };

            this.ExecutionTimeout = 120;
        }

        public bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(this.ConnectionString)
                    && !string.IsNullOrEmpty(this.Sql)
                    && !((this.To == null || !this.To.Any()) && (this.Cc == null || !this.Cc.Any()) && (this.Bcc == null || !this.Bcc.Any()))
                    && !string.IsNullOrEmpty(this.From);
            }
        }

        public override string ToString()
        {
            return string.Format("ConnString:\t {0}\nExec Timeout:\t {13}\nSql:\t\t {1}\nFrom:\t\t {2}\nTo:\t\t {3}\nCc:\t\t {4}\nBcc:\t\t {5}\nSubject:\t {6}\nBody:\t\t {7}\nHtmlBody:\t {8}\nSmtpServer:\t {9}\nSmtpPort:\t {10}\nSmtpUsername:\t {11}\nSmtpPassword:\t {12}", this.ConnectionString, this.Sql, this.From, this.To.AsCommaSeparated(), this.Cc.AsCommaSeparated(), this.Bcc.AsCommaSeparated(), this.Subject, this.Body, this.HtmlBody, this.SmtpServer, this.SmtpPort, this.SmtpUsername, this.SmtpPassword, this.ExecutionTimeout);
        }
    }
}
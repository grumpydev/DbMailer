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

        public Settings()
        {
            this.Subject = "DbMailer Email";
            this.Body = string.Empty;
            this.HtmlBody = false;

            this.To = new string[] { };
            this.Cc = new string[] { };
            this.Bcc = new string[] { };
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
    }
}
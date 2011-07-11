namespace DbMailer
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using System.Net;
    using System.Net.Mail;

    public class CsvMailer
    {
        private readonly Settings settings;

        public CsvMailer(Settings settings)
        {
            this.settings = settings;
        }

        public bool Process()
        {
            var data = this.GetResultsTable().ToCsv();

            var tempFileName = this.GenerateTempFile(data);

            try
            {
                this.SendEmail(tempFileName);
            }
            finally
            {
                File.Delete(tempFileName);
            }

            return true;
        }

        private void SendEmail(string tempFileName)
        {
            using (var mailMessage = this.BuildMessage())
            {
                mailMessage.Attachments.Add(new Attachment(tempFileName));

                var client = this.BuildClient();

                client.Send(mailMessage);
            }
        }

        private SmtpClient BuildClient()
        {
            var client = new SmtpClient("localhost", 25);

            if (!String.IsNullOrEmpty(this.settings.SmtpServer))
            {
                client.Host = this.settings.SmtpServer;
            }

            if (this.settings.SmtpPort != null)
            {
                client.Port = this.settings.SmtpPort.Value;
            }

            if (!String.IsNullOrEmpty(this.settings.SmtpUsername))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(this.settings.SmtpUsername, this.settings.SmtpPassword);
            }

            return client;
        }

        private MailMessage BuildMessage()
        {
            var mailMessage = new MailMessage
                {
                    From = new MailAddress(this.settings.From),
                    Subject = this.settings.Subject,
                    Body = this.settings.Body.Replace(@"\n", "\r\n"),
                    IsBodyHtml = this.settings.HtmlBody
                };

            foreach (var recipient in this.settings.To)
            {
                mailMessage.To.Add(new MailAddress(recipient));
            }

            foreach (var recipient in this.settings.Cc)
            {
                mailMessage.CC.Add(new MailAddress(recipient));
            }

            foreach (var recipient in this.settings.Bcc)
            {
                mailMessage.To.Add(new MailAddress(recipient));
            }

            return mailMessage;
        }

        private string GenerateTempFile(string data)
        {
            var tempFileName = Path.GetTempPath() + Guid.NewGuid() + DateTime.Now.Ticks + ".csv";

            File.WriteAllText(tempFileName, data);

            return tempFileName;
        }

        private DataTable GetResultsTable()
        {
            using (var connection = new SqlConnection(this.settings.ConnectionString))
            {
                using (var adapter = new SqlDataAdapter(this.settings.Sql, connection))
                {
                    using (var resultsTable = new DataTable())
                    {
                        connection.Open();

                        adapter.Fill(resultsTable);

                        return resultsTable;
                    }
                }
            }
        }
    }
}
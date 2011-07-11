namespace DbMailer.Tests
{
    using Should;

    using Xunit;

    public class SettingsTests
    {
        private Settings settings;

        public SettingsTests()
        {
            this.settings = new Settings
                {
                    ConnectionString = "Connection String",
                    Sql = "Sql",
                    From = "From",
                    To = new[] { "to" },
                    Cc = new[] { "cc" },
                    Bcc = new[] { "bcc" },
                    Subject = "Subject",
                    Body = "Body",
                    SmtpServer = "SmtpServer",
                    SmtpPort = 25,
                    SmtpUsername = "SmtpUsername",
                    SmtpPassword = "SmtpPassword",
                };
        }

        [Fact]
        public void Should_be_valid_with_all_fields()
        {
            settings.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Should_be_invalid_with_no_connection_string()
        {
            settings.ConnectionString = null;

            settings.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Should_be_invalid_with_no_sql()
        {
            settings.Sql = null;

            settings.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Should_be_invalid_with_no_from()
        {
            settings.From = null;

            settings.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Should_be_invalid_with_no_recipient_collections()
        {
            settings.To = null;
            settings.Cc = null;
            settings.Bcc = null;

            settings.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Should_be_invalid_with_no_recipients()
        {
            settings.To = new string[] { };
            settings.Cc = new string[] { };
            settings.Bcc = new string[] { };

            settings.IsValid.ShouldBeFalse();
        }

        [Fact]
        public void Should_be_valid_with_just_to_recipients()
        {
            settings.To = new string[] { "Address" };
            settings.Cc = new string[] { };
            settings.Bcc = new string[] { };

            settings.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Should_be_valid_with_just_cc_recipients()
        {
            settings.To = new string[] { };
            settings.Cc = new string[] { "Address" };
            settings.Bcc = new string[] { };

            settings.IsValid.ShouldBeTrue();
        }

        [Fact]
        public void Should_be_valid_with_just_bcc_recipients()
        {
            settings.To = new string[] { };
            settings.Cc = new string[] { };
            settings.Bcc = new string[] { "Address" };

            settings.IsValid.ShouldBeTrue();
        }

    }
}
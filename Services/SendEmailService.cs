using System.Net;
using System.Net.Mail;

namespace SendEmailApi.Services
{
    public  class SendEmailService
    {
        private readonly IConfiguration _configuration;

        public SendEmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public async Task SendEmail(string from, List<string> destination, string subject, string body)
        {
            foreach (var dest in destination)
            {
                MailMessage mail = new MailMessage(from, dest );
                mail.From = new MailAddress(from);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = _configuration.GetSection("ConfigurationSmtpServer").GetValue<string>("SmtpHost");
                smtp.Port = _configuration.GetSection("ConfigurationSmtpServer").GetValue<int>("SmtpPort"); ;
                smtp.Credentials = new NetworkCredential(_configuration.GetSection("ConfigurationSmtpServer").GetValue<string>("SmtpUserName"),
                                                         _configuration.GetSection("ConfigurationSmtpServer").GetValue<string>("SmtpPassword"));
                smtp.EnableSsl = _configuration.GetSection("ConfigurationSmtpServer").GetValue<bool>("EnableSsl"); ;
                await smtp.SendMailAsync(mail);
                smtp.Dispose();
                
            }
        }
    }
}

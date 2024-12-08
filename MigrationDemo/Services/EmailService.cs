using System.Net.Mail;
using System.Net;

namespace MigrationDemo.Services
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string toEmail, string subject, string body);
    }
    public class EmailService : IEmailService
    {
        
            private readonly string _smtpServer = "smtp.gmail.com";
            private readonly int _smtpPort = 587;
            private readonly string _fromEmail = "gurukoushik08@gmail.com"; 
            private readonly string _password = "gaem fffz tucz atvq";  

            public async Task<bool> SendEmailAsync(string toEmail, string subject, string body)
            {
                try
                {
                    var smtpClient = new SmtpClient(_smtpServer)
                    {
                        Port = _smtpPort,
                        Credentials = new NetworkCredential(_fromEmail, _password),
                        EnableSsl = true,
                    };

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_fromEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                    };
                    mailMessage.To.Add(toEmail);

                    await smtpClient.SendMailAsync(mailMessage);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }
}

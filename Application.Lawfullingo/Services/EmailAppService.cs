using Application.Lawfullingo.Services.SMTPS;
using ApplicationContract.Lawfullingo.IApplicationService.Services;
using Dynamic.Domain.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Application.Lawfullingo.Services
{
    public class EmailAppService : IEmailAppService
    {

        private readonly SmtpSettings _smtp;

        public EmailAppService(IOptions<SmtpSettings> smtpSettings)
        {
            _smtp = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string bodyHtml)
        {
            using (var client = new SmtpClient(_smtp.Host)
            {
                Port = _smtp.Port,
                Credentials = new NetworkCredential(_smtp.SenderEmail, _smtp.SenderPassword),
                EnableSsl = _smtp.EnableSsl,
            })
            using (var message = new MailMessage(_smtp.SenderEmail, toEmail, subject, bodyHtml)
            {
                IsBodyHtml = true
            })
            {
                await client.SendMailAsync(message);
            }
        }








        //public void SendEmail(string recipientEmail, string subject, string htmlBody)
        //{
        //    string senderEmail = "lawfullingo@gmail.com";
        //    string senderPassword = "uqmtdfyyphhbizuu";

        //    SmtpClient smtpClient = new SmtpClient("smtppro.zoho.in")
        //    {
        //        Port = 587,
        //        Credentials = new NetworkCredential(senderEmail, senderPassword),
        //        EnableSsl = true,
        //    };

        //    MailMessage mailMessage = new MailMessage(senderEmail, recipientEmail)
        //    {
        //        Subject = subject,
        //        IsBodyHtml = true,
        //        Body = htmlBody
        //    };

        //    try
        //    {
        //        smtpClient.Send(mailMessage);

        //        Console.WriteLine("Email sent successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Email could not be sent: " + ex.Message);
        //    }

        //}


    }
}

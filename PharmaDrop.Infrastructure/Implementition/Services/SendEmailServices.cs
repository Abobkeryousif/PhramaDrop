using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using PharmaDrop.Application.Contract.Services;
using PharmaDrop.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaDrop.Infrastructure.Implementition.Services
{
    public class SendEmailServices : ISendEmailServices
    {
        private readonly MailSetting _mailSetting;
        public SendEmailServices(IOptions<MailSetting> mailSetting)
        {
            _mailSetting = mailSetting.Value;
        }
        public void SendEmail(string email, string subject, string message)
        {
            using (var Client = new SmtpClient())
            {
                Client.Connect(_mailSetting.Host,_mailSetting.Port);
                Client.Authenticate(_mailSetting.Email,_mailSetting.Password);

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = message,
                    TextBody = "Hello"
                };

                var Message = new MimeMessage
                {
                    Body = bodyBuilder.ToMessageBody()
                };

                Message.From.Add(new MailboxAddress("PharmaDrop",_mailSetting.Email));
                Message.To.Add(new MailboxAddress("Hello",email));
                Message.Subject = subject;
                Client.Send(Message);
                Client.Disconnect(true);
            }
        }
    }
}

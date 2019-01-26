using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyMeetUp.Logic.Infrastructure
{
    public class ServerSettings
    {
        public ServerSettings(string login, string password, string smtp)
        {
            Login = login;
            Password = password;
            Smtp = smtp;
        }

        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Smtp { get; private set; }
    }

    public class SendEmail
    {
        public void SendSmtpEmail(ServerSettings serverSettings, MyMeetupEmail email)
        {
            SmtpClient smtpClient = GetSmtpClient(serverSettings);
            MailMessage message = CreateEmailMessage(email);
            smtpClient.Send(message);
        }

        private MailMessage CreateEmailMessage(MyMeetupEmail email)
        {
            MailMessage message = new MailMessage(email.From, email.To, email.Subject, email.Body);
            message.IsBodyHtml = true;
            if (!string.IsNullOrEmpty(email.ReplyTo))
            {

                message.ReplyToList.Add(email.ReplyTo);

            }

            return message;
        }

        public Task SendSmtpEmailAsync(ServerSettings serverSettings, MyMeetupEmail email)
        {

            SmtpClient smtpClient = GetSmtpClient(serverSettings);
            MailMessage message = CreateEmailMessage(email);
            
            return smtpClient.SendMailAsync(message);
            
        }

        private static SmtpClient GetSmtpClient(ServerSettings serverSettings)
        {
            int port = 25; string smtpAdress = serverSettings.Smtp;
            int pos = serverSettings.Smtp.IndexOf(":");
            if (pos > 0)
            {
                smtpAdress = serverSettings.Smtp.Substring(0, pos);
                port = Convert.ToInt32(serverSettings.Smtp.Substring(pos + 1));
            }
            SmtpClient smtpClient = new SmtpClient
            {
                Host = smtpAdress,
                Port = port,
                EnableSsl = true, // TODO : surely not totally correct
                Credentials = new NetworkCredential(serverSettings.Login, serverSettings.Password)
            };
            return smtpClient;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Infrastructure
{
    public class EmailSender :IEmailSender
    {
        private IConfiguration _configuration;
        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SendEmail se = new SendEmail();
            MyMeetupEmail me=new MyMeetupEmail(subject,htmlMessage,email,_configuration["emailContact"]);
            return se.SendSmtpEmailAsync(EmailSender.GetSettings(_configuration),me);
        }

        

        public static ServerSettings GetSettings(IConfiguration configuration)
        {
            return new ServerSettings(configuration["emailSettings:login"], configuration["emailSettings:password"],
                configuration["emailSettings:smtp"]);
        }
    }
}

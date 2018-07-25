using System;
using System.Collections.Generic;
using System.Text;

namespace MyMeetUp.Logic.Infrastructure
{
   public class MyMeetupEmail
    {
        public string Subject { get; set; }

        public string Body { get; set; }

        public string To { get; set; }

        public string From { get; set; }

        public string ReplyTo { get; set; }

        public MyMeetupEmail(string subject, string body, string to, string @from)
        {
            Subject = subject;
            Body = body;
            To = to;
            From = @from;
        }

        public MyMeetupEmail(string subject, string to, string @from)
        {
            Subject = subject;
            To = to;
            From = @from;
        }

        public MyMeetupEmail()
        {
        }

        public MyMeetupEmail(string subject, string body, string to, string @from, string replyTo)
        {
            Subject = subject;
            Body = body;
            To = to;
            From = @from;
            ReplyTo = replyTo;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class ConfigurationModel
    {
        public string SmtpServer { get; set; }

        public string SmtpLogin { get; set; }

        public string SmtpPassword { get; set; }
    }
}

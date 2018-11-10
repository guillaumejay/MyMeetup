using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class ConfigurationModel
    {
        public string SmtpServer { get; set; }

        public string SmtpLogin { get; set; }

        public string SmtpPassword { get; set; }

        public HomePageDTO HomePage { get; set; }
    }
}

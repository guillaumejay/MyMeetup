using MyMeetUp.Logic.Entities;
using System.Collections.Generic;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class AdminMeetupModel : Meetup
    {
        public List<CharterContent> Contents { get; set; }

    }
}

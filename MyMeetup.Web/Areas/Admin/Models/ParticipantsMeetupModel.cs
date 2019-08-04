using System.Collections.Generic;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class ParticipantsMeetupModel
    {
        public List<RegisteredUserModel> Participants { get; set; } = new List<RegisteredUserModel>();
        public string Title { get; set; }
    }
}

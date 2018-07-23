using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class ParticipantsMeetupModel
    {
        public List<MyMeetupUser> Participants { get; set; } = new List<MyMeetupUser>();
        public string Title { get; set; }
    }
}

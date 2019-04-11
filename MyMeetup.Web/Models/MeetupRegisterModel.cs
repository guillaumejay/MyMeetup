using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMeetUp.Logic.Entities;

namespace MyMeetup.Web.Models
{
    public class MeetupRegisterModel
    {
        public MeetupRegisterModel()
        {

        }

        public MeetupRegisterModel(Meetup meetup)
        {
            Meetup = meetup;
            if (meetup!=null)
                MeetupId = meetup.Id;
        }

        public string UserEmail { get; set; }
        public int MeetupId { get; set; }
        public Meetup Meetup { get; set; }

        public int AdultNumber { get; set; }

        public int ChildrenNumber { get; set; }

        public string AccomodationChoice { get; set; }

        public string Notes { get; set; }
        public List<SelectListItem> PossibleAccomodations{ get; set; }

        public string AccomodationId { get; set; }
        public List<string> Errors { get; set; } =new List<string>();
    }
}

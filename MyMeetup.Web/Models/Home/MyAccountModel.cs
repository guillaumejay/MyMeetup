using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;
using System.Collections.Generic;

namespace MyMeetup.Web.Models.Home
{
    public class MyAccountModel
    {
        public MyAccountModel()
        {
        }

        public MyMeetupUser CurrentUser { get; set; }
        public string NextRegistrations { get; set; }

        public List<Registration> OldMeetups { get; set; }

        public List<NextMeetupView> NextMeetups=new List<NextMeetupView>();

        public MyAccountModel(MyMeetupUser user)
        {
            CurrentUser = user;
        }

    }

    public class NextMeetupView
    {
        public int MeetupId { get; set; }

        public string MeetupDate { get; set; }

        public bool IsAlreadyRegistered { get; set;}

        public NextMeetupView()
        {
        }

        public NextMeetupView(Meetup meetup)
        {
            MeetupId = meetup.Id;
            MeetupDate = $"Du {meetup.StartDate:dd/MM/yyyy} au {meetup.EndDate:dd/MM/yyyy}";
        }
    }
}

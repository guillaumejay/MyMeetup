using System;
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

        public string EmailContactText
        {
            get
        {
            if (CurrentUser.IsOkToGetMeetupsInfo)
            {
                return "Tu souhaites recevoir des emails de notre part pour nos prochaines rencontres";
            }

            return "Tu NE souhaites PAS recevoir d'emails de notre part sur nos prochaines rencontres";
        }
        }

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

        public string Title { get; set; }

        public bool CanRegister { get; set;}


        public NextMeetupView()
        {
        }

        public NextMeetupView(Meetup meetup)
        {
            MeetupId = meetup.Id;
            Title = meetup.Title;
            MeetupDate = $"Du {meetup.StartDate:dd/MM/yyyy} au {meetup.EndDate:dd/MM/yyyy}";
            CanRegister = meetup.IsVisible &&  meetup.OpenForRegistrationOn.HasValue &&
                          meetup.OpenForRegistrationOn.Value <= DateTime.Now.Date;
            
        }
    }
}

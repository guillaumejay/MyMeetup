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
                return "Actuellement, tu souhaites recevoir nos emails pour les prochaines rencontres";
            }

            return "Actuellement, tu NE souhaites PAS recevoir nos emails pour les prochaines rencontres";
        }
        }

        public bool IsOkToGetEmail => CurrentUser.IsOkToGetMeetupsInfo;

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

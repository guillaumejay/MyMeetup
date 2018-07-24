using MyMeetUp.Logic.Entities;

namespace MyMeetUp.Logic.Models
{
     public class MeetupAdminShortModel
    {
        public MeetupAdminShortModel()
        {
        }

        public static MeetupAdminShortModel FromMeetup(Meetup meetup)
        {
            MeetupAdminShortModel m = new MeetupAdminShortModel
            {
                MeetupId = meetup.Id,
                IsVisible = meetup.IsVisible,
                OpenForRegistrationOn = meetup.OpenForRegistrationOn?.ToString("dd MMM yyyy")??string.Empty,
                Title = meetup.Title
            };
            return m;
        }

        public int MeetupId { get; set; }

        public string Title { get; set; }

        public bool IsVisible { get; set; }

        public string OpenForRegistrationOn { get; set; }

        public int RegistrationCount { get; set; }


    }
}

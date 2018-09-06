using System.Collections.Generic;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Models.Home
{
    public class IndexModel
    {

        public Meetup Meetup { get; set; }

        public List<CharterContent> Charter { get; set; }=new List<CharterContent>();

        public SigninMeetupModel SigninMeetupModel { get; set; }
    }
}

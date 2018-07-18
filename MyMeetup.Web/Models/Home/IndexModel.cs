using System.Collections.Generic;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Models.Home
{
    public class IndexModel
    {

        public Meetup Rencontre { get; set; }

        public List<CharterContent> Charter { get; set; }=new List<CharterContent>();
    }
}

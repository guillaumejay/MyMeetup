using System.Collections.Generic;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class UserDetailsModel
    {
        public int Id { get; set; }

        public string Referrer { get; set; }

       public List<RegisteredMeetupModel> Meetups { get; set; }=new List<RegisteredMeetupModel>();

       public List<Payment> Payments { get; set; } = new List<Payment>();
    }

  
}

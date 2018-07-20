using System;
using System.Collections.Generic;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class AdminMeetupModel:Meetup
    {
        public List<MyMeetupUser> Participants { get; set; }=new List<MyMeetupUser>();

    }
}

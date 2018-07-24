using System;
using System.Collections.Generic;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Areas.Admin.Models
{
    public class AdminIndexModel
    {
        public List<MeetupAdminShortModel> Meetups { get; set; } =new List<MeetupAdminShortModel>();

        public string HelloText { get; set; }
    }
}

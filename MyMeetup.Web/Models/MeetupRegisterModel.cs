using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMeetUp.Logic.Entities;

namespace MyMeetup.Web.Models
{
    public class MeetupRegisterModel
    {
        public Meetup Meetup { get; set; }

        public int AdultNumber { get; set; }

        public int ChildrenNumber { get; set; }

        public string AccomodationChoice { get; set; }

        public string Notes { get; set; }
        public List<SelectListItem> PossibleAccomodations{ get; set; }

        public string AccomodationId { get; set; }
    }
}

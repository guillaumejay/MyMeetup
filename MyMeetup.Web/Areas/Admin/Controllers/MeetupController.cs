using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MeetupController:BaseController
    {
        public MeetupController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager) : base(domain,
            userManager)
        {
        }

        public IActionResult Index(int meetupId,[FromServices] IMapper mapper)
        {
            var model = mapper.Map<AdminMeetupModel>(Domain.GetMeetup(meetupId, true));
            model.Participants = Domain.GetParticipantsFor(meetupId);
            return View(model);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public IActionResult Index([BindRequired,FromQuery]int  id ,[FromServices] IMapper mapper)
        {
            var model = mapper.Map<AdminMeetupModel>(Domain.GetMeetup(id, true));
            model.Participants = Domain.GetParticipantsFor(id);
            return View(model);
         
        }
    }
}

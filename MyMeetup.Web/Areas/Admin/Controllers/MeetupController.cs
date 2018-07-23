using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetUp.Logic.Entities;
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

        public IActionResult Index()
        {
            AdminIndexModel aim = new AdminIndexModel();
            aim.Meetups.AddRange(Domain.GetAdminMeetup());
            return View(aim);
        }


        public IActionResult Details([BindRequired]int  id ,[FromServices] IMapper mapper)
        {
            var model = CreateModel(id, mapper);
            return View(model);
        }

        public IActionResult Participants([BindRequired]int id)
        {
            var model = CreateModel(id);
            return View(model);
        }

        private AdminMeetupModel CreateModel(int id, IMapper mapper)
        {
            var model = mapper.Map<AdminMeetupModel>(Domain.GetMeetup(id, true));
    
            return model;
        }
        private ParticipantsMeetupModel CreateModel(int id)
        {
            var m = Domain.GetMeetup(id, true);
            var model = new ParticipantsMeetupModel
            {
                Title = m.Title,
                Participants = Domain.GetParticipantsFor(id)
            };
            return model;
        }
        [HttpPost]
        public IActionResult Details(Meetup meetup, [FromServices] IMapper mapper)
        {
            if (!ModelState.IsValid)
            {
                return View(CreateModel(meetup.Id, mapper));
            }
            int id = Domain.AddOrUpdateMeetup(meetup);
            var model = CreateModel(id, mapper);
            return View(model);
        }
    }
}

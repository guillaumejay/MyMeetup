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
            var model = GetMeetupDetailModel(id, mapper);
            return View(model);
        }

        public IActionResult Participants([BindRequired]int id)
        {
            var model = CreateModel(id);
            return View(model);
        }

        private AdminMeetupModel GetMeetupDetailModel(int id, IMapper mapper)
        {
            var model = mapper.Map<AdminMeetupModel>(Domain.GetMeetup(id, true));
            var charters = Domain.GetCharterFor(id,true, false, true).ToList();
            charters.Add(new CharterContent { Position = charters.Count() + 1,MeetupId= id });
            model.Contents = charters;
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
        public IActionResult DetailsMeetup(Meetup meetup, [FromServices] IMapper mapper)
        {
            if (!ModelState.IsValid)
            {
                return View("Details", GetMeetupDetailModel(meetup.Id, mapper));
            }

            int id = Domain.AddOrUpdateMeetup(meetup);
            var model = GetMeetupDetailModel(id, mapper);
            return View("Details", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(CharterContent charter, [FromServices] IMapper mapper)
        {
            if (!ModelState.IsValid)
                return View( GetMeetupDetailModel(charter.MeetupId.Value,mapper));
            Domain.AddOrUpdateCharter(charter);

            return View(GetMeetupDetailModel(charter.MeetupId.Value, mapper));
        }
    }
}

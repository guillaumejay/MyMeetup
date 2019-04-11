using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetup.Web.Infrastructure;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = MyMeetupRole.Administrateur)]
    public class MeetupController : BaseController
    {
        public MeetupController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemetryClient) : base(domain,
            userManager, telemetryClient)
        {
        }

        public IActionResult Index()
        {
            AdminIndexModel aim = new AdminIndexModel();
            aim.Meetups.AddRange(Domain.GetAdminMeetup());
            return View(aim);
        }


        public IActionResult Details([BindRequired]int id, [FromServices] IMapper mapper)
        {
            AdminMeetupModel model = GetMeetupDetailModel(id, mapper);
            return View(model);
        }

        public IActionResult Copy([BindRequired]int id, [FromServices] IMapper mapper)
        {
            AdminMeetupModel model = GetMeetupDetailModel(id, mapper);
            model.Id = 0;
            model.Title = "Copie de " + model.Title;
            model.StartDate = DateTime.Now.AddMonths(1);
            model.EndDate = model.StartDate.AddDays(7);
            model.OpenForRegistrationOn = null;
            return View("Details", model);
        }

        public IActionResult Participants([BindRequired]int id)
        {
            var model = CreateModel(id);
            return View(model);
        }

        private AdminMeetupModel GetMeetupDetailModel(int id, IMapper mapper)
        {
            AdminMeetupModel model = mapper.Map<AdminMeetupModel>(Domain.GetMeetup(id, true));
            List<CharterContent> charters = Domain.GetCharterFor(id, true, false, true).ToList();
            charters.Add(new CharterContent { Position = charters.Count() + 1, MeetupId = id });
            model.Contents = charters;
            return model;
        }

        private AdminMeetupModel GetMeetupDetailModel(Meetup meetup, IMapper mapper)
        {
            AdminMeetupModel model = mapper.Map<AdminMeetupModel>(meetup);
            List<CharterContent> charters = Domain.GetCharterFor(meetup.Id, true, false, true).ToList();
            charters.Add(new CharterContent { Position = charters.Count() + 1, MeetupId = meetup.Id });
            model.Contents = charters;
            return model;
        }
        private ParticipantsMeetupModel CreateModel(int meetupId)
        {
            var accomodations = AccomodationModel.DefaultAccomodationModels;
            var m = Domain.GetMeetup(meetupId, true);
            List<MyMeetupUser> participants = Domain.GetParticipantsFor(meetupId);
            var model = new ParticipantsMeetupModel
            {
                Title = m.Title,

            };
            List<Registration> regs = Domain.GetRegistrationsFor(meetupId, true).ToList();
            foreach (var user in participants)
            {
                var info = new RegisteredUserModel
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id
                };
                var reg = regs.First(x => x.UserId == user.Id);
                info.RegCode = reg.RegistrationCode;
                info.RegisteredOn = reg.CreatedAt;
                info.AdultNumber = reg.NumberOfAdults;
                info.KidNumber = reg.NumberOfChildren;
                info.Accomodation = accomodations.SingleOrDefault(x => x.Id == reg.AccomodationId)?.Description??"";
                info.RegistrationId = reg.Id;
                model.Participants.Add(info);
            }
            return model;
        }
        [HttpPost]
        public IActionResult DetailsMeetup(Meetup meetup, [FromServices] IMapper mapper)
        {

            AdminMeetupModel model;
            if (!ModelState.IsValid)
            {
                model = GetMeetupDetailModel(meetup, mapper);
                Tools.TransferModalStateError(model.Errors, ModelState);

                return View("Details", model);
            }

            int id = Domain.AddOrUpdateMeetup(meetup);
            model = GetMeetupDetailModel(id, mapper);
            return View("Details", model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Details(CharterContent charter, [FromServices] IMapper mapper)
        {
            if (!ModelState.IsValid)
                return View(GetMeetupDetailModel(charter.MeetupId.Value, mapper));
            Domain.AddOrUpdateCharter(charter);

            return View(GetMeetupDetailModel(charter.MeetupId.Value, mapper));
        }
    }
}

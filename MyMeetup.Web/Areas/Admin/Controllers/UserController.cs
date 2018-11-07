using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetup.Web.Models.Components;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = MyMeetupRole.Administrateur)]
    public class UserController : BaseController
    {
        public UserController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager,
            TelemetryClient telemetryClient) : base(domain, userManager, telemetryClient)
        {
        }

        public IActionResult Index()
        {
            var model=new UsersModel();

            List<MyMeetupUser> participants = Domain.GetUsers(true).ToList();
                    Dictionary<int, IGrouping<int, Registration>> regs = Domain.GetRegistrations( true).OrderBy(x=>x.UserId).ThenBy(x=>x.CreatedAt).GroupBy(x=>x.UserId)
                         .ToDictionary(x=>x.Key);
            foreach (var user in participants)
            {
                var info = new UserModel(user);
                IGrouping<int, Registration> userReg = regs[user.Id];
                info.NbMetupsRegistered = userReg.Count();
                info.RegisteredOn = userReg.FirstOrDefault()?.CreatedAt;
              
                model.Users.Add(info);
            }
            return View(model);
        }
        public IActionResult Details(int userId)
        {
            UserDetailsModel udm = new UserDetailsModel { Id = userId, Referrer = Request.Headers["Referer"].ToString() };
            return View(udm);
        }

        public IActionResult Edit(UserEditModel model, string chkSupprimer, [FromServices] IMapper mapper)
        {
            bool aSupprimer = chkSupprimer == "on";
            if (aSupprimer)
            {
                Domain.DeleteUserTotally(model.Id, UserManager);
                return Redirect(model.BackUrl);
            }
            else
            {
                MyMeetupUser user = mapper.Map<MyMeetupUser>(model);
                Domain.ModifyUser(user);
            }

            return View("Details", new UserDetailsModel { Id = model.Id, Referrer = model.BackUrl });
        }
    }
}

using System.Linq;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = MyMeetupRole.Administrateur)]
    public class CharterController : BaseController
    {
        public CharterController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemetryClient) : base(domain, userManager,telemetryClient)
        {
        }

        public IActionResult Index()
        {
            AdminCharterModel m = GetModel();
            return View(m);
        }

        private AdminCharterModel GetModel()
        {
            var charters = Domain.GetCharterFor(null,true, false, true).ToList();
            charters.Add(new CharterContent {Position = charters.Count()+1});
            return new AdminCharterModel {Contents = charters};
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CharterContent charter)
        {
            if (!ModelState.IsValid)
                return View("Index",GetModel());
            Domain.AddOrUpdateCharter(charter);

            return View("Index", GetModel());
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CharterController : BaseController
    {
        public CharterController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager) : base(domain, userManager)
        {
        }

        public IActionResult Index()
        {
            AdminCharterModel m = GetModel();
            return View(m);
        }

        private AdminCharterModel GetModel()
        {
            var charters = Domain.GetCharterFor(null, false, true).ToList();
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
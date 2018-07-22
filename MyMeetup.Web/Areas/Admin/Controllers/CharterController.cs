using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
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
            AdminCharterModel m = new AdminCharterModel {Contents = Domain.GetCharterFor(null, true)};
            return View(m);
        }
    }
}
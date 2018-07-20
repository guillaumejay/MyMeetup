using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : BaseController
    {
        public HomeController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager) : base(domain, userManager)
        {
        }

        public IActionResult Index()
        {
            AdminIndexModel aim=new AdminIndexModel();
            aim.Meetups.AddRange(Domain.GetAdminMeetup());
            return View(aim);
        }
    }
}
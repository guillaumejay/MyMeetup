using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = MyMeetupRole.Administrateur)]
    [Area("Admin")]
    public class HomeController : BaseController
    {
        public HomeController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemtryClient) : base(domain, userManager, telemtryClient)
        {
        }

        public IActionResult Index()
        {
            AdminIndexModel aim = new AdminIndexModel();
            aim.Meetups.AddRange(Domain.GetAdminMeetup());
            
            aim.HelloText = CurrentUser.FirstName;
            // When I said I was a specially crafted software, I was not joking...
            if (CurrentUser.FirstName == "Lori")
            {
                aim.HelloText = $"<i class=\"fa fa-heart\" style='color:pink;font-size:24px'> </i>{aim.HelloText}<i class=\"fa fa-heart\"  style='color:pink;font-size:24px'></i>";
            }

            return View(aim);
        }
    }
}
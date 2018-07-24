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
        public HomeController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager) : base(domain, userManager)
        {
        }

        public IActionResult Index()
        {
            AdminIndexModel aim=new AdminIndexModel();
            aim.Meetups.AddRange(Domain.GetAdminMeetup());
            // When I said I was a specially crafted software, I was not joking...

            aim.HelloText = CurrentUser.FirstName;
            if (CurrentUser.FirstName == "Lori")
            {
                aim.HelloText = $"<i class=\"fa fa-heart\" style='color:pink;font-size:24px'> </i>{aim.HelloText}<i class=\"fa fa-heart\"  style='color:pink;font-size:24px'></i>";
            }

            return View(aim);
        }
    }
}
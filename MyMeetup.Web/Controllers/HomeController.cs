using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Models;
using MyMeetup.Web.Models.Home;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Controllers
{
    public class HomeController : BaseController
    {
        private UserManager<MyMeetupUser> _userManager;

        public HomeController(MyMeetupDomain domain,UserManager<MyMeetupUser> userManager) : base(domain)
        {
            userManager = _userManager;
        }

        public IActionResult Index()
        {
            int rencontreId = 1;
            var model = new IndexModel
            {
                Rencontre = Domain.ObteniRencontre(rencontreId, true),
                Charter = Domain.ObtenirChartePour(rencontreId,true)
            };

            return View(model);
        }

        [HttpPost]
        public void SigninMeetup(SigninMeetupModel model)
        {
            
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

   
}

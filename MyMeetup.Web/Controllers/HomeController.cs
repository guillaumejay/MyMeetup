using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetup.Web.Models;
using MyMeetup.Web.Models.Home;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Controllers
{
    public class HomeController : BaseController
    {
       

        public HomeController(MyMeetupDomain domain,UserManager<MyMeetupUser> userManager) : base(domain,userManager)
        {
          
        }

        public IActionResult Index()
        {
            int rencontreId = 1;
            var model = new IndexModel
            {
                Rencontre = Domain.GetMeetup(rencontreId, true),
                Charter = Domain.GetCharterFor(rencontreId,true)
            };

            return View(model);
        }

        public IActionResult Connected()
        {
            return View("Connected", GetConnectedModel());

        }

        [HttpPost]
        public ActionResult SigninMeetup(SigninMeetupModel model,[FromServices]SignInManager<MyMeetupUser> signInManager)
        {
           int id = Domain.AddRegularUser(model,UserManager);
            var user = UserManager.FindByEmailAsync(model.Email).Result;
             signInManager.SignInAsync(user, isPersistent: true);
            return View("Connected", GetConnectedModel(user));
        }

        private ConnectedModel GetConnectedModel(MyMeetupUser currentUser=null)
        {
            ConnectedModel cm=new ConnectedModel();
            cm.CurrentUser =currentUser?? CurrentUser;
            var meetup = Domain.GetNextMeetupsFor(cm.CurrentUser.Id, true).OrderBy(x => x.StartDate).FirstOrDefault();
            if (meetup == null)
            {
                cm.NextMeetupText = "Vous n'avez aucune rencontre de prévue !";
            }
            else
            {
                cm.NextMeetupText =
                    $"Votre prochaine rencontre est {meetup.Title}, à partir du {meetup.StartDate.ToString("dd MMMM yyyy")}";
            }
            return cm;
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

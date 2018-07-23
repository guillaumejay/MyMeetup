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
                Meetup = Domain.GetMeetup(rencontreId, true),
                Charter = Domain.GetCharterFor(rencontreId,false,true,true).ToList()
            };

            return View(model);
        }

        public IActionResult MyAccount()
        {
            return View("MyAccount", GetMyAccountModel());

        }

        [HttpPost]
        public ActionResult SigninMeetup(SigninMeetupModel model,[FromServices]SignInManager<MyMeetupUser> signInManager)
        {
           int id = Domain.AddRegularUser(model,UserManager);
            var user = UserManager.FindByEmailAsync(model.Email).Result;
             signInManager.SignInAsync(user, isPersistent: true);
            return View("MyAccount", GetMyAccountModel(user));
        }

        private MyAccountModel GetMyAccountModel(MyMeetupUser currentUser=null)
        {
            MyAccountModel cm =new MyAccountModel(currentUser ?? CurrentUser);
            
            var meetup = Domain.GetMeetupsFor(cm.CurrentUser.Id, true).OrderBy(x => x.StartDate).FirstOrDefault();
            if (meetup == null)
            {
                cm.NextMeetupText = "Vous n'avez aucune rencontre de prévue !";
            }
            else
            {
                string regCode = Domain.GetRegistrationCode(cm.CurrentUser.Id, meetup.Id);
                cm.NextMeetupText =
                    $"Votre prochaine rencontre est {meetup.Title}, à partir du {meetup.StartDate:dd MMMM yyyy}<br/>Votre code d'invitation est  :{regCode}";
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

using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyMeetup.Web.Infrastructure;
using MyMeetup.Web.Models;
using MyMeetup.Web.Models.Home;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;
using System;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using MyMeetUp.Logic.Entities;

namespace MyMeetup.Web.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        //private IMapper _mapper;
        public Meetup CurrentMeetup;
        public HomeController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemetryClient/*,IMapper mapper*/) : base(domain, userManager, telemetryClient)
        {
            CurrentMeetup = Domain.GetNextMeetup(DateTime.Now, true);
          //  _mapper = mapper;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {


            return View(CreateLandingPageModel());
        }

        private IndexModel CreateLandingPageModel(SigninMeetupModel signinMeetupModel = null)
        {
            IndexModel model = new IndexModel();

            if (CurrentMeetup != null)
            {
                model.Meetup = CurrentMeetup;
                model.Charter = Domain.GetCharterFor(CurrentMeetup.Id, false, true, true).ToList();
                model.SigninMeetupModel = signinMeetupModel;
            }
            
            else
            {
                model.Meetup=new Meetup();
                model.Meetup.Title = Parameters.Value.HomeTitle;
                model.Meetup.PublicDescription = Parameters.Value.HomeContent;
                model.Meetup.TitleImage = Parameters.Value.HomeImage;
            }

            return model;
        }
        [HttpGet("Me")]
        public IActionResult MyAccount([FromServices]IConfiguration configuration)
        {
            return View("MyAccount", GetMyAccountModel(configuration));

        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(SigninMeetupModel model, [FromServices]SignInManager<MyMeetupUser> signInManager, [FromServices]IConfiguration configuration)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", CreateLandingPageModel( model));
            }

            MyMeetupUser user = UserManager.FindByEmailAsync(model.Email.Trim()).Result;
            if (user != null)
            {
                return View("MyAccount", GetMyAccountModel(configuration, user));
            }
            var result = Domain.AddRegularUser(model, UserManager);
            if (result.UserOk)
            {
                user = UserManager.FindByEmailAsync(model.Email).Result;
                SendEmail se = new SendEmail();
                MyMeetupEmail email = new MyMeetupEmail("Nouvel inscrit",
                    configuration["emailContact"], configuration["emailContact"]);
                //TODO Ugly, should be templated
                email.Body = $"{model.FirstName} {model.Name} - {model.Email} {model.PhoneNumber}";
                if (!string.IsNullOrEmpty(result.RegistrationCode))
                {
                    var meetup = Domain.GetMeetup(model.MeetupId.Value, true);
                    email.Body += Environment.NewLine +
                                  $"Inscrit(e) à {meetup.Title} et son code d'enregistrement est {result.RegistrationCode}";
                    email.ReplyTo = model.Email;
                }

                try
                {
                    if (!Debugger.IsAttached)
                    {
                        se.SendSmtpEmail(EmailSender.GetSettings(configuration), email);
                    }
                }
                catch (Exception e)
                {
                    _telemetryClient.TrackException(e);
                }


                //   signInManager.SignInAsync(user, isPersistent: true);
                return View("MyAccount", GetMyAccountModel(configuration, user));
            }

            return View("Index", CreateLandingPageModel());
        }

        private MyAccountModel GetMyAccountModel(IConfiguration configuration, MyMeetupUser currentUser = null)
        {
            MyAccountModel cm = new MyAccountModel(currentUser ?? CurrentUser);

            MyMeetUp.Logic.Entities.Meetup meetup = Domain.GetMeetupsFor(cm.CurrentUser.Id, true).OrderBy(x => x.StartDate).FirstOrDefault();
            if (meetup == null)
            {
                cm.NextMeetupText = "Vous n'avez aucune rencontre de prévue !";
            }
            else
            {
                string regCode = Domain.GetRegistrationCode(cm.CurrentUser.Id, meetup.Id);
                cm.NextMeetupText =
                    $"Tu es préinscrit-e à {meetup.Title}, à partir du {meetup.StartDate:dd MMMM yyyy}.<br/>" +
                $"Nous te confirmerons très prochainement cette pré-inscription, par mail (envoyé à {cm.CurrentUser.Email})."
                    +
                    "<br/>contact@rencontresnonscos.org";

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

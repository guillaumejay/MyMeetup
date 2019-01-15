using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyMeetup.Web.Infrastructure;
using MyMeetup.Web.Models;
using MyMeetup.Web.Models.Home;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Entities.Enums;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

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
                model.Meetup = new Meetup();
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
                return View("Index", CreateLandingPageModel(model));
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
            MyAccountModel model = new MyAccountModel(currentUser ?? CurrentUser);

            List<Registration> regs = Domain.GetRegistrations(model.CurrentUser.Id, true)
                .Where(x => x.RegistrationStatus <= ERegistrationStatus.Registered)
                .OrderBy(x => x.Meetup.StartDate).ToList();
            model.OldMeetups = regs.Where(x => x.Meetup.EndDate <= DateTime.Now).ToList();
            var nextRegistrations = regs.Where(x => x.Meetup.StartDate > DateTime.Now).ToList();
            if (nextRegistrations.Any() == false)
            {
           
                model.NextRegistrations = "Tu n'as aucune rencontre de prévue !";
            }
            else
            {
                model.NextRegistrations = "Tu es pré-inscrit-e à : <ul> ";
                List<string> texts = new List<string>();
                foreach (var reg in regs)
                {
                    string tmp = $"<li>{reg.Meetup.Title} (qui commence le {reg.Meetup.StartDate:dd MMMM yyyy}) : ";
                    if (reg.RegistrationStatus == ERegistrationStatus.Preregistration)
                    {
                        tmp += $"ton code d'enregistrement sera envoyé à {model.CurrentUser.Email}";
                    }
                    else
                    {
                        tmp += $"ton code d'enregistrement est {reg.RegistrationCode}";
                    }
                    texts.Add(tmp+ "</li>");
                }

                model.NextRegistrations += Environment.NewLine + String.Join("", texts) + Environment.NewLine +  "</ul>";
                List<Meetup> meetups = Domain.GetNextMeetups(DateTime.Now.Date,true);
                foreach (Meetup m in meetups)
                {
                    var vm=new NextMeetupView(m);
                    vm.IsAlreadyRegistered = regs.Any(x => x.MeetupId == vm.MeetupId);
                    model.NextMeetups.Add(vm);
                }
            }
            
            return model;
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

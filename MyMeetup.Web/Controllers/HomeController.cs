﻿using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyMeetup.Web.Models;
using MyMeetup.Web.Models.Home;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.ApplicationInsights;
using MyMeetup.Web.Infrastructure;

namespace MyMeetup.Web.Controllers
{
    public class HomeController : BaseController
    {
        public int CurrentRencontreId = 1;
        public HomeController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemetryClient) : base(domain, userManager, telemetryClient)
        {

        }

        public IActionResult Index()
        {
            

            return View(CreateIndexModel(CurrentRencontreId));
        }

        private IndexModel CreateIndexModel(int rencontreId)
        {
            IndexModel model = new IndexModel
            {
                Meetup = Domain.GetMeetup(rencontreId, true),
                Charter = Domain.GetCharterFor(rencontreId, false, true, true).ToList()
            };
            return model;
        }
        [HttpGet]
        public IActionResult MyAccount([FromServices]IConfiguration configuration)
        {
            return View("MyAccount", GetMyAccountModel(configuration));

        }

        [HttpPost]
        public ActionResult Index(SigninMeetupModel model, [FromServices]SignInManager<MyMeetupUser> signInManager, [FromServices]IConfiguration configuration)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",CreateIndexModel(CurrentRencontreId));

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
                //TODO Ugly template
                email.Body = $"{model.FirstName} {model.Name} - {model.Email} {model.PhoneNumber}";
                if (!string.IsNullOrEmpty(result.RegistrationCode))
                {
                    var meetup = Domain.GetMeetup(model.MeetupId.Value, true);
                    email.Body += Environment.NewLine +
                                  $"Inscrit(e) à {meetup.Title} et son code d'enregistrement est {result.RegistrationCode}";
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

            return View("Index", CreateIndexModel(CurrentRencontreId));
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
                $"Nous vous confirmerons très prochainement cette pré-inscription, par mail (envoyé à {cm.CurrentUser.Email})."
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

using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using MyMeetup.Web.Areas.Admin.Models;
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
        private IMapper _mapper;
        public Meetup CurrentMeetup;
        public HomeController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemetryClient, IMapper mapper) : base(domain, userManager, telemetryClient)
        {
            CurrentMeetup = Domain.GetNextMeetup(DateTime.Now, true);
        }

        private List<SelectListItem> CreateSelectListForAcc(List<AccomodationModel> accomodations)
        {
            var list = new List<SelectListItem>();
            
            foreach (var a in accomodations)
            {
               list.Add(new SelectListItem(a.Description, a.Id));
            }

            return list;
        }

        [AllowAnonymous]
        public IActionResult Charter()
        {
            AdminCharterModel m = new AdminCharterModel();
            var contents = Domain.GetCharterFor(null, false, true, true).ToList();
            contents.Add(new CharterContent { Position = contents.Count() + 1 });
            m.Contents = contents;
            return View(m);
        }


        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(CreateLandingPageModel());
        }

        [Route("register/{meetupId:int}")]
        public IActionResult Register(int meetupId)
        {
            var rm = CreateModelForRegistration(meetupId);
            if (rm.Meetup.IsVisible == false || rm.Meetup.EndDate <= DateTime.Now.Date ||
                rm.Meetup.OpenForRegistrationOn.HasValue == false ||
                rm.Meetup.OpenForRegistrationOn.Value > DateTime.Now.Date)
            {
                return Redirect("/me");
            }
            return View(rm);
        }

        private MeetupRegisterModel CreateModelForRegistration(int meetupId, bool onlyActiveAcc = true)
        {
            MeetupRegisterModel rm = new MeetupRegisterModel(Domain.GetMeetup(meetupId, true));
            rm.UserEmail = CurrentUser.Email;
            FillAccomodations(rm, onlyActiveAcc);
       
            return rm;
        }

        private void FillAccomodations(MeetupRegisterModel rm, bool onlyActiveAcc)
        {
            var possible = AccomodationModel.DefaultAccomodationModels;
            rm.PossibleAccomodations = CreateSelectListForAcc( (onlyActiveAcc) ? possible.Where(x => x.IsActive).ToList() : possible);
        }

        private MeetupRegisterModel CreateModelForRegistration(MeetupRegisterModel model,bool onlyActiveAcc=true)
        {
            MeetupRegisterModel rm = _mapper.Map<MeetupRegisterModel>(model);
            rm.UserEmail = CurrentUser.Email;
            FillAccomodations(rm,true);
            return rm;
        }
        [HttpPost("postRegister")]

        public IActionResult PostRegister(MeetupRegisterModel model, [FromServices]IConfiguration configuration)
        {
            if (ModelState.IsValid)
            {
                var accomodations = AccomodationModel.DefaultAccomodationModels;
                Registration r = new Registration(CurrentUser.Id, model.MeetupId)
                {
                    AccomodationId = model.AccomodationId,
                    Notes = model.Notes,
                    NumberOfAdults = model.AdultNumber,
                    NumberOfChildren = model.ChildrenNumber,
                    RegistrationStatus = ERegistrationStatus.Registered
                };
                Domain.AddOrUpdateRegistration(r);
                string body = "Bonjour :-) <br/><br/>";
                body += "Voici une réservation à partir du site de Rencontres Non-Scos :<br/><br/>";
                body += $"Prénom : {CurrentUser.FirstName}<br/>  Nom : {CurrentUser.LastName} <br/>Email : {CurrentUser.Email}<br/>";
                body +=
                    $"Logement : {accomodations.Single(x => x.Id == r.AccomodationId).Id}<br/>Adultes : {r.NumberOfAdults} <br/>Enfants : {r.NumberOfChildren}<br/>";
                if (!string.IsNullOrWhiteSpace(r.Notes))
                {
                    body += "Notes :<hr>";
                    body += r.Notes;
                    body += "<hr>";

                }

                body += "<br/>Cordialement";
                SendEmail se = new SendEmail();
                Meetup m = Domain.GetMeetup(model.MeetupId, true);
                MyMeetupEmail email = new MyMeetupEmail("{CurrentUser.FirstName} {CurrentUser.LastName} s'inscrit à " + m.Title, body, m.MeetupPlaceAdminEmail ?? configuration["emailContact"],
                    configuration["emailContact"]);
                if (!string.IsNullOrEmpty(m.MeetupPlaceAdminEmail))
                {
                    email.BCC = configuration["emailContact"];
                }

                if (Debugger.IsAttached)
                {
                    email.To = configuration["emailContact"];
                    email.Subject = "TEST " + email.Subject;
                }

                email.ReplyTo = CurrentUser.Email;
                email.CC = CurrentUser.Email;
                SendEmail s = new SendEmail();
                se.SendSmtpEmail(EmailSender.GetSettings(configuration), email);
                return Redirect("/me");
            }
            MeetupRegisterModel rm = CreateModelForRegistration(model);

            Tools.TransferModalStateError(rm.Errors, ModelState);

            return View("Register",rm);
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
                model.Meetup = new Meetup
                {
                    Title = Parameters.Value.HomeTitle,
                    PublicDescription = Parameters.Value.HomeContent,
                    TitleImage = Parameters.Value.HomeImage
                };
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
            var result = Domain.AddRegularUser(model, null, UserManager);
            if (result.UserOk)
            {
                user = UserManager.FindByEmailAsync(model.Email).Result;
                SendEmail se = new SendEmail();
                MyMeetupEmail email = new MyMeetupEmail($"Nouvel Adhérent {model.FirstName} {model.Name} ",
                    configuration["emailContact"], configuration["emailContact"])
                {
                    Body = $"Prénom :{model.FirstName} <br/>Nom :{model.Name} - <br/>Email : {model.Email} <br/>Tel: {model.PhoneNumber}"
                };
                //TODO Ugly, should be templated
                if (!string.IsNullOrEmpty(result.RegistrationCode))
                {
                    var meetup = Domain.GetMeetup(model.MeetupId.Value, true);
                    email.Body += Environment.NewLine +
                                  $"Inscrit(e) à {meetup.Title} et son code d'enregistrement est {result.RegistrationCode}";

                }
                email.ReplyTo = model.Email;
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

                signInManager.SignInAsync(user, isPersistent: true).Wait();
                //   signInManager.SignInAsync(user, isPersistent: true);
                if (model.MeetupId.HasValue)
                {

                    return RedirectToAction("Register", new { meetupId = model.MeetupId.Value });
                }

                return RedirectToAction("MyAccount");
                //     return View("MyAccount", GetMyAccountModel(configuration, user));
            }

            return View("Index", CreateLandingPageModel());
        }

        [HttpGet("ChangeEmailContact")]
        public ActionResult ChangeEmailContact()
        {
            CurrentUser.IsOkToGetMeetupsInfo = !CurrentUser.IsOkToGetMeetupsInfo;
            Domain.ModifyUser(CurrentUser);
            return RedirectToAction("MyAccount");
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
                        tmp += $"ton code d'enregistrement est {reg.RegistrationCode} pour {reg.NumberOfAdults} adultes, {reg.NumberOfChildren} dans {reg.AccomodationId}";
                    }
                    texts.Add(tmp + "</li>");
                }

                model.NextRegistrations += Environment.NewLine + String.Join("", texts) + Environment.NewLine + "</ul>";

            }
            List<Meetup> meetups = Domain.GetNextMeetups(DateTime.Now.Date, true);
            foreach (Meetup m in meetups)
            {
                var vm = new NextMeetupView(m);
                if (!regs.Any(x => x.MeetupId == vm.MeetupId))
                {
                    model.NextMeetups.Add(vm);
                }
            }
            return model;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyMeetup.Web.Models;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Controllers
{
    [Authorize]
    public class RegisterController : BaseController
    {
        public List<SelectListItem> Accomodations = new List<SelectListItem>();
            
        public IActionResult Index()
        {
            return RedirectPermanent("/me");
        }

        public RegisterController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemetryClient) : base(domain, userManager, telemetryClient)
        {
            Accomodations.Add(new SelectListItem("EmplacemetCamping","1"));
            Accomodations.Add(new SelectListItem("Landette 4 couchages", "2"));
            Accomodations.Add(new SelectListItem("Landette 6 couchages", "3"));
            Accomodations.Add(new SelectListItem("Chalet", "4"));
            Accomodations.Add(new SelectListItem("Mobilhome Docinela", "5"));
            Accomodations.Add(new SelectListItem("Mobilhome Pichonela", "6"));
            Accomodations.Add(new SelectListItem("Mobilhome Hacienda", "7"));
            Accomodations.Add(new SelectListItem("Yourte", "8"));
            Accomodations.Add(new SelectListItem("Gîte T4", "9"));
            Accomodations.Add(new SelectListItem("Chambre dans Gîte T6", "10"));

        }

        public IActionResult Index(int meetupId)
        {
            MeetupRegisterModel rm=new MeetupRegisterModel();
            rm.Meetup = Domain.GetMeetup(meetupId, true);
            rm.PossibleAccomodations = Accomodations;
            return View(rm);
        }
    }
}
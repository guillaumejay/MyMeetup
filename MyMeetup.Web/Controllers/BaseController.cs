using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;
using System;


namespace MyMeetup.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly TelemetryClient _telemetryClient;
        protected MyMeetupDomain Domain;
        protected UserManager<MyMeetupUser> UserManager;
        protected BaseController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager, TelemetryClient telemetryClient)
        {
            Domain = domain;
            UserManager = userManager;
            _telemetryClient = telemetryClient;
        }

        // protected int? CurrentId => User.Identity.GetUserId();
        protected MyMeetupUser CurrentUser => UserManager.GetUserAsync(HttpContext.User).Result;

        protected Lazy<AppParameter> Parameters => new Lazy<AppParameter>(() => { return Domain.GetAppParameter(true); });

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            ViewBag.AppTitle = Parameters.Value.Title;
            ViewBag.ContactEmail = "contact@rencontresnonscos.org";
        }
    }
}

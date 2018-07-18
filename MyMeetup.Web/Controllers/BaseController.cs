using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyMeetUp.Logic.Infrastructure;


namespace MyMeetup.Web.Controllers
{
    public abstract class BaseController:Controller
    {
        protected MyMeetupDomain Domain;
        protected UserManager<MyMeetupUser> UserManager;
        protected BaseController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager)
        {
            Domain = domain;
            UserManager = userManager;
        }

       // protected int? CurrentId => User.Identity.GetUserId();
        protected MyMeetupUser CurrentUser=>  UserManager.GetUserAsync(HttpContext.User).Result;
    }
}

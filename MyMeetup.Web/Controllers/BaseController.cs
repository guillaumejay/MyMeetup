using Microsoft.AspNetCore.Mvc;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Controllers
{
    public abstract class BaseController:Controller
    {
        protected MyMeetupDomain Domain;

        protected BaseController(MyMeetupDomain domain)
        {
            Domain = domain;
        }
    }
}

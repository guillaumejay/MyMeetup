using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMeetUp.Logic.Infrastructure;

namespace Rencontres.Web.Controllers
{
    public abstract class BaseController:Controller
    {
        protected MyMeetupDomain Domaine;

        protected BaseController(MyMeetupDomain domaine)
        {
            Domaine = domaine;
        }
    }
}

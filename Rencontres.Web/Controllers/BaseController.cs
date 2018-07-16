using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rencontres.Metier.Infrastructure;

namespace Rencontres.Web.Controllers
{
    public abstract class BaseController:Controller
    {
        protected RencontresDomaine Domaine;

        protected BaseController(RencontresDomaine domaine)
        {
            Domaine = domaine;
        }
    }
}

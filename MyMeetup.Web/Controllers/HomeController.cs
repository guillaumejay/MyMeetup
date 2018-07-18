using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyMeetUp.Logic.Infrastructure;
using Rencontres.Web.Models;
using Rencontres.Web.Models.Home;

namespace Rencontres.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(MyMeetupDomain domaine) : base(domaine)
        {
        }

        public IActionResult Index()
        {
            int rencontreId = 1;
            var model = new IndexModel
            {
                Rencontre = Domaine.ObteniRencontre(rencontreId, true),
                Charter = Domaine.ObtenirChartePour(rencontreId,true)
            };

            return View(model);
        }

        [HttpPost]
        public void SigninMeetup(SigninMeetupModel model)
        {

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

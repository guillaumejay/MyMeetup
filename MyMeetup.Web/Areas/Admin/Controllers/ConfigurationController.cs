using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetup.Web.Infrastructure;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = MyMeetupRole.Administrateur)]
    public class ConfigurationController : BaseController
    {
        private IConfiguration _configuration;
        public ConfigurationController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager,IConfiguration configuration) : base(domain, userManager)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            ConfigurationModel m = GetModel();
            return View(m);
        }

        private ConfigurationModel GetModel()
        {
            var model = new ConfigurationModel();
            var settings = EmailSender.GetSettings(_configuration);
            model.SmtpServer = settings.Smtp;
            model.SmtpLogin = settings.Login;
            model.SmtpPassword = settings.Password.Length + " caractéres";
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index(CharterContent charter)
        {
            if (!ModelState.IsValid)
                return View("Index",GetModel());
            Domain.AddOrUpdateCharter(charter);

            return View("Index", GetModel());
        }
    }
}
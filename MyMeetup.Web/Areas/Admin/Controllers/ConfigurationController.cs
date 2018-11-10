using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Controllers;
using MyMeetup.Web.Infrastructure;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = MyMeetupRole.Administrateur)]
    public class ConfigurationController : BaseController
    {
        private IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ConfigurationController(MyMeetupDomain domain, UserManager<MyMeetupUser> userManager,IConfiguration configuration,IMapper mapper,TelemetryClient telemtryClient) : base(domain, userManager,telemtryClient)

        {
            _configuration = configuration;
            _mapper = mapper;
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
            var parameters=Domain.GetAppParameter(true);
            model.HomePage = _mapper.Map<HomePageDTO>(parameters);
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditHome(HomePageDTO homePageSetup)
        {
            if (!ModelState.IsValid)
                return View("Index",GetModel());


            Domain.UpdateHomePage(homePageSetup);

            return View("Index", GetModel());
        }
    }
}
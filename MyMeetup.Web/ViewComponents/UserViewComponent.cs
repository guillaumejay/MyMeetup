using AutoMapper;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Infrastructure;
using System.Threading.Tasks;
using MyMeetup.Web.Models.Components;

namespace MyMeetup.Web.ViewComponents
{
    [ViewComponent]
    public class UserViewComponent : BaseViewComponent
    {
        protected UserManager<MyMeetupUser> UserManager;

        protected IMapper Mapper;

        public UserViewComponent(MyMeetupDomain domain, TelemetryClient telemetryClient, UserManager<MyMeetupUser> userManager, IMapper mapper) : base(domain, telemetryClient)
        {
            UserManager = userManager;
            Mapper = mapper;
        }

        public async Task<IViewComponentResult> InvokeAsync(int userId,string referrer)
        {
            var user = await UserManager.Users.SingleOrDefaultAsync(x => x.Id == userId);
            UserEditModel model = Mapper.Map<UserEditModel>(user);
            model.BackUrl = referrer;
            
            return View(model);
        }
    }
}

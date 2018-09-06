using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.ViewComponents
{
    public abstract class BaseViewComponent:ViewComponent
    {
        protected readonly TelemetryClient TelemetryClient;
        protected MyMeetupDomain Domain;

        protected BaseViewComponent(MyMeetupDomain domain, TelemetryClient telemetryClient)
        {
            Domain = domain;

            TelemetryClient = telemetryClient;
        }

    }
}

using AutoMapper;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meetup, AdminMeetupModel>();
        }
    }
}

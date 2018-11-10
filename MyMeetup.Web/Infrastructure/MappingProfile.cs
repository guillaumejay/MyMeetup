using AutoMapper;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetup.Web.Models.Components;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure;
using MyMeetUp.Logic.Models;

namespace MyMeetup.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Meetup, AdminMeetupModel>();
            CreateMap<AppParameter, HomePageDTO>();
            CreateMap<MyMeetupUser,UserEditModel>().ReverseMap();
        }
    }
}

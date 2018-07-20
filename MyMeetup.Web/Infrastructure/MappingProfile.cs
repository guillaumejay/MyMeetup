using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MyMeetup.Web.Areas.Admin.Models;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetup.Web.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MyMeetupUser, AdminMeetupModel>();
        }
    }
}

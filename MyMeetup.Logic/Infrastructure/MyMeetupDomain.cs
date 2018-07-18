using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Models;

namespace MyMeetUp.Logic.Infrastructure
{
    public class MyMeetupDomain
    {
        private readonly MyMeetupContext _context;

        public MyMeetupDomain(MyMeetupContext context)
        {
            _context = context;
        }

        #region Meetup

        public Meetup GetMeetup(int id, bool readOnly)
        {
            IQueryable<Meetup> q = _context.Meetups.Where(x => x.Id == id);
            if (readOnly)
            {
                q = q.AsNoTracking();
            }

            return q.FirstOrDefault();
        }

        public int Register(MyMeetupUser user, int meetupId)
        {
            Registration r =
                new Registration(user.Id, meetupId) { RegistrationCode = Registration.CreateCode(user, meetupId) };
            _context.Registrations.Add(r);
            _context.SaveChanges();
            return r.Id;
        }

        public IQueryable<Meetup> GetNextMeetupsFor(int userId, bool readOnly)
        {
            var q = _context.Registrations.Where(x => x.UserId == userId).Select(x => x.Meetup);
            if (readOnly)
                q = q.AsNoTracking();
            return q;
        }


        #endregion

        #region Charter

        public List<CharterContent> GetCharterFor(int? meetupId, bool readOnly)
        {

            if (meetupId.HasValue == false)
            {
                meetupId = -1;
            }

            IQueryable<CharterContent> q = _context.CharterContents.Where(x => (x.MeetupId == null || x.MeetupId == meetupId.Value)
                                                        && x.IsActive);
            if (readOnly)
            {
                q = q.AsNoTracking();
            }

            return q.ToList();
        }
        #endregion

        public int AddRegularUser(SigninMeetupModel model, UserManager<MyMeetupUser> userManager)
        {
            MyMeetupUser newUser = MyMeetupUser.From(model);
            int? id = MyMeetupUser.CreateUser(newUser, MyMeetupRole.Participant,
                newUser.Initials +
                DateTime.Now.ToString("HHmmss"), userManager);
            if (id.HasValue && model.MeetupId.HasValue)
            {
                Register(newUser, model.MeetupId.Value);
            }
            return id.Value;
        }


    }
}

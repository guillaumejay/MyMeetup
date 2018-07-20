using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure.DataContexts;
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

        public int? Register(MyMeetupUser user, int meetupId, bool ignoreMeetupRegistrationValidity=false)
        {
            Meetup m = _context.Meetups.AsNoTracking().FirstOrDefault(x => x.Id == meetupId);
            if (m == null)
                return null;
            if (!ignoreMeetupRegistrationValidity)
            {
                if (m.IsVisible == false)
                    return null;
                if (m.OpenForRegistrationOn == null)
                    return null;
                if (m.OpenForRegistrationOn.Value > DateTime.UtcNow)
                    return null;
            }
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
        public string GetRegistrationCode(int userId, int meetupId)
        {
            var reg = _context.Registrations.AsNoTracking()
                .FirstOrDefault(x => x.MeetupId == meetupId && x.UserId == userId);
            return reg?.RegistrationCode;
        }

        #endregion

        #region Charter

        public List<CharterContent> GetCharterFor(int? meetupId, bool readOnly)
        {

            if (meetupId.HasValue == false)
            {
                meetupId = -1;
            }

            IQueryable<CharterContent> q = _context.CharterContents.Where(x =>
                (x.MeetupId == null || x.MeetupId == meetupId.Value)
                && x.IsActive).OrderBy(x => x.MeetupId).ThenBy(x => x.Position);
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


        public IEnumerable<MeetupAdminShortModel> GetAdminMeetup()
        {
            var meetups = _context.Meetups.AsNoTracking();
            var list = new List<MeetupAdminShortModel>();
            List<Tuple<int, int>> perMeetup = _context.Registrations.AsNoTracking().GroupBy(x => x.MeetupId)
                    .Select(group => Tuple.Create(group.Key, group.Count())).ToList();
                
            foreach (var m in meetups)
            {
                var model = MeetupAdminShortModel.FromMeetup(m);
                // TODO quick ugly thing
                model.RegistrationCount=  perMeetup.SingleOrDefault(x => x.Item1 == m.Id)?.Item2 ?? 0;
                list.Add(model);
            }

            return list;
        }

        public List<MyMeetupUser> GetParticipantsFor(int meetupId)
        {
            var list = _context.Users
                .Where(u => _context.Registrations.Where(r => r.MeetupId == meetupId).Select(x => x.UserId).ToList()
                    .Contains(u.Id)).AsNoTracking().ToList();
            return list;
        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure.DataContexts;
using MyMeetUp.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<MyMeetupUser> GetParticipantsFor(int meetupId)
        {
            List<int> userIs = _context.Registrations.Where(r => r.MeetupId == meetupId).Select(x => x.UserId).ToList();
            List<MyMeetupUser> list = _context.Users
                .Where(u => userIs.Contains(u.Id)).AsNoTracking().ToList();
            return list;
        }
        public Meetup GetMeetup(int id, bool readOnly)
        {
            IQueryable<Meetup> q = _context.Meetups.Where(x => x.Id == id);
            if (readOnly)
            {
                q = q.AsNoTracking();
            }

            return q.FirstOrDefault();
        }

        public Registration Register(MyMeetupUser user, int meetupId, bool ignoreMeetupRegistrationValidity = false)
        {
            Meetup m = _context.Meetups.AsNoTracking().FirstOrDefault(x => x.Id == meetupId);
            if (m == null)
            {
                return null;
            }

            if (!ignoreMeetupRegistrationValidity)
            {
                if (m.IsVisible == false)
                {
                    return null;
                }

                if (m.OpenForRegistrationOn == null)
                {
                    return null;
                }

                if (m.OpenForRegistrationOn.Value > DateTime.UtcNow)
                {
                    return null;
                }
            }
            Registration r =
                new Registration(user.Id, meetupId) { RegistrationCode = Registration.CreateCode(user, meetupId) };
            _context.Registrations.Add(r);
            _context.SaveChanges();
            return r;
        }

        public IQueryable<Meetup> GetMeetupsFor(int userId, bool readOnly)
        {
            IQueryable<Meetup> q = _context.Registrations.Where(x => x.UserId == userId).Select(x => x.Meetup);
            if (readOnly)
            {
                q = q.AsNoTracking();
            }

            return q;
        }
        public string GetRegistrationCode(int userId, int meetupId)
        {
            Registration reg = _context.Registrations.AsNoTracking()
                .FirstOrDefault(x => x.MeetupId == meetupId && x.UserId == userId);
            return reg?.RegistrationCode;
        }

        public IEnumerable<MeetupAdminShortModel> GetAdminMeetup()
        {
            IQueryable<Meetup> meetups = _context.Meetups.AsNoTracking();
            List<MeetupAdminShortModel> list = new List<MeetupAdminShortModel>();
            List<Tuple<int, int>> perMeetup = _context.Registrations.AsNoTracking().GroupBy(x => x.MeetupId)
                .Select(group => Tuple.Create(group.Key, group.Count())).ToList();

            foreach (Meetup m in meetups)
            {
                MeetupAdminShortModel model = MeetupAdminShortModel.FromMeetup(m);
                // TODO quick ugly thing
                model.RegistrationCount = perMeetup.SingleOrDefault(x => x.Item1 == m.Id)?.Item2 ?? 0;
                list.Add(model);
            }


            return list;
        }


        public int AddOrUpdateMeetup(Meetup meetup)
        {
            if (meetup.Id == 0)
            {
                _context.Meetups.Add(meetup);
            }
            else
            {
                meetup.CreatedAt = _context.Meetups.AsNoTracking().First(x => x.Id == meetup.Id).CreatedAt;
                meetup.UpdatedAt = DateTime.UtcNow;
                _context.Update(meetup);
            }
            _context.SaveChanges();

            return meetup.Id;
        }

        #endregion

        #region Charter
        public int AddOrUpdateCharter(CharterContent charter)
        {
            if (charter.Id == 0)
            {
                _context.CharterContents.Add(charter);
            }
            else
            {
                charter.CreatedAt = _context.CharterContents.AsNoTracking().First(x => x.Id == charter.Id).CreatedAt;
                charter.UpdatedAt = DateTime.UtcNow;
                _context.Update(charter);
            }
            _context.SaveChanges();
            IOrderedQueryable<CharterContent> qcharters = GetCharterFor(charter.MeetupId, true, false, false);

            List<CharterContent> charters = qcharters.ThenByDescending(x => x.UpdatedAt).ToList();
            for (int i = 0; i < charters.Count(); i++)
            {
                charters[i].Position = i + 1;
            }
            _context.SaveChanges();
            return charter.Id;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="meetupId"></param>
        /// <param name="exclusive">only return those corresponding to meetupId</param>
        /// <param name="onlyActive"></param>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public IOrderedQueryable<CharterContent> GetCharterFor(int? meetupId, bool exclusive, bool onlyActive, bool readOnly)
        {
            IQueryable<CharterContent> q;
            if (exclusive)
            {
                q = _context.CharterContents.Where(x => x.MeetupId == meetupId);
            }
            else
            {
                q = _context.CharterContents.Where(x =>
                 (x.MeetupId == null || x.MeetupId == meetupId.Value));
            }

            if (onlyActive)
            {
                q = q.Where(x => x.IsActive);
            }

            if (readOnly)
            {
                q = q.AsNoTracking();
            }

            return q.OrderBy(x => x.MeetupId).ThenBy(x => x.Position);
        }
        #endregion

        public ResultAddRegularUser AddRegularUser(SigninMeetupModel model, UserManager<MyMeetupUser> userManager)
        {
          ResultAddRegularUser rau=new ResultAddRegularUser();
            MyMeetupUser newUser = MyMeetupUser.From(model);
            rau.UserId = MyMeetupUser.CreateUser(newUser, MyMeetupRole.Participant,
                   newUser.Initials +
                   DateTime.Now.ToString("HHmmss"), userManager);
            if (rau.UserOk && model.MeetupId.HasValue)
            {
                rau.RegistrationCode = Register(newUser, model.MeetupId.Value).RegistrationCode;
            }

            return rau;
        }



    }
}

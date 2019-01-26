using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure.DataContexts;
using MyMeetUp.Logic.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MyMeetUp.Logic.Entities.Enums;

namespace MyMeetUp.Logic.Infrastructure
{
    public class MyMeetupDomain
    {
        private readonly MyMeetupContext _context;

        public MyMeetupDomain(MyMeetupContext context)
        {
            _context = context;
        }

        /// <summary>
        /// one line by app/tenant
        /// </summary>
        /// <param name="readOnly"></param>
        /// <returns></returns>
        public AppParameter GetAppParameter(bool @readOnly)
        {
            //TODO : add caching
            var q= _context.AppParameters.AsQueryable();
            if (readOnly)
            {
                q = q.AsNoTracking();

            }
            return q.First();
        }

        public void UpdateHomePage(HomePageDTO homePageSetup)
        {
            var parameters = GetAppParameter(false);
            parameters.HomeImage = homePageSetup.HomeImage;
            parameters.HomeTitle = homePageSetup.HomeTitle;
            parameters.HomeContent = homePageSetup.HomeContent;
            _context.SaveChanges();
        }

        #region Meetup

        public Meetup GetNextMeetup(DateTime fromDate,bool readOnly)
        {
            IQueryable<Meetup> q = _context.Meetups.Where(m =>
                m.StartDate > fromDate && m.IsVisible && m.OpenForRegistrationOn.HasValue &&
                m.OpenForRegistrationOn <= fromDate);
            if (readOnly)
                q = q.AsNoTracking();
            return q.OrderBy(x => x.OpenForRegistrationOn).FirstOrDefault();
        }
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
                new Registration(user.Id, meetupId) { RegistrationCode = Registration.CreateCode(user.Id, meetupId) };
            _context.Registrations.Add(r);
            _context.SaveChanges();
            return r;
        }

        public IQueryable<Registration> GetRegistrations(int userId, bool readOnly)
        {
            IQueryable<Registration> q = _context.Registrations.Include(x=>x.Meetup)
                .Where(x => x.UserId == userId)
                ;
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
        public IQueryable<Registration> GetRegistrations(bool readOnly)
        {
            var q = _context.Registrations.AsQueryable();
            if (readOnly)
            {
                q = q.AsNoTracking();
            }

            return q;
        }

        public IQueryable<Registration> GetRegistrationsFor(int meetupId, bool readOnly)
        {
            var q = GetRegistrations(readOnly).Where(x => x.MeetupId == meetupId);
            return q;
        }

        public void DeleteUserTotally(int userId, UserManager<MyMeetupUser> userManager)
        {
            var registrations = _context.Registrations.Where(x => x.UserId == userId).ToList();
            _context.RemoveRange(registrations);
            _context.SaveChanges();
            MyMeetupUser user = userManager.Users.First(x => x.Id == userId);
            var result = userManager.DeleteAsync(user).Result;

        }

        public void ModifyUser(MyMeetupUser model)
        {
            var user = _context.Users.Single(x => x.Id == model.Id);
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.IsOkToGetMeetupsInfo = model.IsOkToGetMeetupsInfo;
            _context.SaveChanges();
        }

        public IQueryable<MyMeetupUser> GetUsers(bool isReadOnly)
        {
            var query = _context.Users.AsQueryable();
            if (isReadOnly)
            {
                query = query.AsNoTracking();
            }

            return query;
        }


        public List<RegisteredMeetupModel> GetRegisteredMeetups(int userId)
        {
            List<RegisteredMeetupModel> meetups=new  List<RegisteredMeetupModel>();
            var result = _context.Registrations.Include(x => x.Meetup).Where(x => x.UserId == userId).AsNoTracking()
                .ToList();
            foreach (var r in result)
            {
                var meetup=new RegisteredMeetupModel(r.MeetupId,r.Id,r.UserId, r.Meetup.Title,r.RegistrationCode,r.CreatedAt);
                meetups.Add(meetup);
            }

            return meetups;
        }

        public List<Meetup> GetNextMeetups(DateTime nowDate, bool readOnly)
        {
            var q = _context.Meetups.Where(x => x.EndDate > nowDate && x.IsVisible);
            if (readOnly)
                q = q.AsNoTracking();
            return q.OrderBy(x => x.StartDate).ToList();
        }

        public int AddOrUpdateRegistration(Registration registration)
        {
            var inDb = _context.Registrations.Where(x =>
                    x.MeetupId == registration.MeetupId && x.ReferentUserId == registration.ReferentUserId)
                .SingleOrDefault();
            if (inDb == null)
            {
                registration.RegistrationCode = Registration.CreateCode(registration.UserId, registration.MeetupId);
                Debug.Assert(registration.MeetupId!=0);
                Debug.Assert(registration.UserId != 0);

                registration.ReferentUserId = null;
                _context.Registrations.Add(registration);
                _context.SaveChanges();
            }
            else
            {
                inDb.AccomodationId = registration.AccomodationId;
                inDb.Notes = registration.Notes;
                inDb.NumberOfAdults = registration.NumberOfAdults;
                inDb.NumberOfChildren = registration.NumberOfChildren;
                registration.Id = inDb.Id;
            }

            _context.SaveChanges();
            return registration.Id;
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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

        #region Rencontres

        public Meetup ObteniRencontre(int id, bool lectureSeule)
        {
            IQueryable<Meetup> q = _context.Meetups.Where(x => x.Id == id);
            if (lectureSeule)
            {
                q = q.AsNoTracking();
            }

            return q.FirstOrDefault();
        }

        #endregion

        #region Charte

        public List<CharterContent> ObtenirChartePour(int? rencontreId, bool lectureSeule)
        {

            if (rencontreId.HasValue == false)
            {
                rencontreId = -1;
            }

            IQueryable<CharterContent> q = _context.CharterContents.Where(x => (x.MeetupId == null || x.MeetupId == rencontreId.Value)
                                                        && x.IsActive);
            if (lectureSeule)
            {
                q = q.AsNoTracking();
            }

            return q.ToList();
        }
        #endregion
    }
}

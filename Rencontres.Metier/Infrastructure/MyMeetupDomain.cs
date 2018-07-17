using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Modeles;

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

        public Rencontre ObteniRencontre(int id, bool lectureSeule)
        {
            IQueryable<Rencontre> q = _context.Rencontres.Where(x => x.Id == id);
            if (lectureSeule)
            {
                q = q.AsNoTracking();
            }

            return q.FirstOrDefault();
        }

        #endregion

        #region Charte

        public List<ContenuCharte> ObtenirChartePour(int? rencontreId, bool lectureSeule)
        {

            if (rencontreId.HasValue == false)
            {
                rencontreId = -1;
            }

            IQueryable<ContenuCharte> q = _context.ContenusChartes.Where(x => (x.RencontreId == null || x.RencontreId == rencontreId.Value)
                                                        && x.Actif);
            if (lectureSeule)
            {
                q = q.AsNoTracking();
            }

            return q.ToList();
        }
        #endregion
    }
}

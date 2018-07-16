using Microsoft.EntityFrameworkCore;
using Rencontres.Metier.Modeles;
using System.Collections.Generic;
using System.Linq;

namespace Rencontres.Metier.Infrastructure
{
    public class RencontresDomaine
    {
        private RencontresContext _context;

        public RencontresDomaine(RencontresContext context)
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

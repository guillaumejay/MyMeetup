using System.ComponentModel.DataAnnotations;
using Rencontres.Metier.Infrastructure;

namespace Rencontres.Metier.Modeles
{
    public class Inscription:EntiteDatee
    {
        public int RencontreUserId { get; set; }

        public RencontreUser RencontreUser { get; set; }

        public int RencontreId { get; set; }

        public Rencontre Rencontre { get; set; }

        [StringLength(20)]
        public string CodeReservation { get; set; }

        public decimal MontantVerse { get; set; }
    }
}

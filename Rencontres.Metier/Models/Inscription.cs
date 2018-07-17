using System.ComponentModel.DataAnnotations;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Modeles
{
    public class Inscription:EntityWithDate
    {
        public int RencontreUserId { get; set; }

        public MyMeetupUser MyMeetupUser { get; set; }

        public int RencontreId { get; set; }

        public Rencontre Rencontre { get; set; }

        [StringLength(20)]
        public string CodeReservation { get; set; }

        public decimal MontantVerse { get; set; }
    }
}

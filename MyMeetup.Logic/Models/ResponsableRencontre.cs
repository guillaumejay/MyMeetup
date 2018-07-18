using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Modeles
{
    [Table("ResponsablesRencontres")]
   public class ResponsableRencontre
    {
        [Key(),Column(Order = 0)]
        [ForeignKey(nameof (User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public MyMeetupUser User { get; set; }

        [Key(), Column(Order = 1)]
        [ForeignKey(nameof(Rencontre))]
        public int RencontreId { get; set; }

        [ForeignKey(nameof(RencontreId))]
        public Rencontre Rencontre { get; set; }
    }
}

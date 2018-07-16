using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Rencontres.Metier.Infrastructure;

namespace Rencontres.Metier.Modeles
{
    [Table("ResponsablesRencontres")]
   public class ResponsableRencontre
    {
        [Key(),Column(Order = 0)]
        [ForeignKey(nameof (User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public RencontreUser User { get; set; }

        [Key(), Column(Order = 1)]
        [ForeignKey(nameof(Rencontre))]
        public int RencontreId { get; set; }

        [ForeignKey(nameof(RencontreId))]
        public Rencontre Rencontre { get; set; }
    }
}

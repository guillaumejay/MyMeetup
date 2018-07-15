using System;
using System.ComponentModel.DataAnnotations;

namespace Rencontres.Metier.Modeles
{
    public abstract class EntiteDatee
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreeLe
        {
            get; set;
        }

        public DateTime ModifieLe { get; set; }
    }
}

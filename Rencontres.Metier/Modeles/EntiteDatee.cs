using System;
using System.ComponentModel.DataAnnotations;

namespace Rencontres.Metier.Modeles
{
    public abstract class EntiteDatee
    {
        [Key]
        public int Id { get; set; }

        public DateTime CreationLe
        {
            get; set;
        }

        public DateTime ModificationLe { get; set; }

        protected EntiteDatee()
        {
            CreationLe = DateTime.UtcNow;
            ModificationLe = CreationLe;
        }
    }
}

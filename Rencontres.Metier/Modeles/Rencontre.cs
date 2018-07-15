using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rencontres.Metier.Modeles
{
    public class Rencontre:EntiteDatee
    {

        [StringLength(80)]
        public string Titre { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateDebut { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateFin { get; set; }

        /// <summary>
        /// Visible pour les visiteurs
        /// </summary>
        public bool EstVisible { get; set; }

        public DateTime DateOuvertureInscription { get; set; }

        public int MontantVerse { get; set; }
    }
}

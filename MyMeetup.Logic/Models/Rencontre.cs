using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMeetUp.Logic.Modeles
{
    public class Rencontre : EntityWithDate
    {

        [StringLength(80)]
        [Required]
        public string Titre { get; set; }

        [Column(TypeName = "Date")]
        public DateTime DateDebut { get; set; }
        [Column(TypeName = "Date")]
        public DateTime DateFin { get; set; }

        /// <summary>
        /// Visible pour les visiteurs
        /// </summary>
        public bool EstVisible { get; set; }

        public DateTime OuvertInscriptionLe { get; set; }

        public List<ResponsableRencontre> Responsables { get; set; }

        [Required]
        public string DescriptionPublique { get; set; }
        [Required]
        public string DescriptionInscrit { get; set; }

        public string ImageTitre { get; set; }

    }
}

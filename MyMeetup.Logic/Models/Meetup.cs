using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMeetUp.Logic.Models
{
    public class Meetup : EntityWithDate
    {

        [StringLength(80)]
        [Required]
        public string Title { get; set; }

        [Column(TypeName = "Date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "Date")]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Visible pour les visiteurs
        /// </summary>
        public bool IsVisible { get; set; }

        public DateTime? OpenForRegistrationOn { get; set; }

        public List<MeetupAdmin> MeetupAdmins { get; set; }

        [Required]
        public string PublicDescription { get; set; }
        [Required]
        public string RegisteredDescription { get; set; }

        public string TitleImage { get; set; }

    }
}

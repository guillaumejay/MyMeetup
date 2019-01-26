using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Design;

namespace MyMeetUp.Logic.Entities
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Meetup : EntityWithDate
    {
        private string DebuggerDisplay =>  $"{Id} {Title} {StartDate:yyyy-MM-dd} {EndDate:yyyy-MM-dd}";
        
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

        [EmailAddress]
        public string MeetupPlaceAdminEmail { get; set; }

        [Required]
        public string PublicDescription { get; set; }
        [Required]
        public string RegisteredDescription { get; set; }

        public string TitleImage { get; set; }

        [NotMapped] public string DefaultDateText => $"Du {StartDate:dddd dd MMM yyy} au {EndDate:dddd dd MMM yyyy}";

    }
}

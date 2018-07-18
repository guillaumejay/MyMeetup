using System.ComponentModel.DataAnnotations;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Models
{
    public class Registration:EntityWithDate
    {
        public int RencontreUserId { get; set; }

        public MyMeetupUser MyMeetupUser { get; set; }

        public int RencontreId { get; set; }

        public Meetup Rencontre { get; set; }

        [StringLength(20)]
        public string RegistrationCode { get; set; }

        public decimal PaidFees { get; set; }
    }
}

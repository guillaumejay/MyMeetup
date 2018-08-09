using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using MyMeetUp.Logic.Entities.Enums;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Entities
{
    public class Registration:EntityWithDate
    {
        [ForeignKey(nameof(MyMeetupUser))]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]

        public MyMeetupUser User { get; set; }

        [ForeignKey(nameof(Meetup))]
        public int MeetupId { get; set; }

        public Registration(int userId, int meetupId)
        {
            UserId = userId;
            MeetupId = meetupId;
        }

        [ForeignKey(nameof(MeetupId))]
        public Meetup Meetup { get; set; }

        [StringLength(20)]
        public string RegistrationCode { get; set; }

        [ForeignKey(nameof(ReferentUser))]
        public int? ReferentUserId { get; set; }

        [ForeignKey(nameof(ReferentUserId))]
        public MyMeetupUser ReferentUser { get; set; }

        public decimal PaidFees { get; set; }

        public static string CreateCode(MyMeetupUser user, int meetupId)
        {
            string code = string.Join("",$"{user.Id:0000}{user.Initials}{meetupId:000}".Reverse());
            return code;
        }

        public ERegistrationStatus RegistrationStatus { get; set; }

        [StringLength(250)]
        public string Notes { get; set; }
    }
}

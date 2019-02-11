using System;
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

        public Registration()
        {
            RegistrationStatus = ERegistrationStatus.Preregistration;
        }
        public Registration(int userId, int meetupId):this()
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
        [Obsolete]
        public int NumberOfPersons { get; set; }

        [Range(0, 10)]
        public int NumberOfChildren { get; set; }

        [Range(0, 10)]
        public int NumberOfAdults { get; set; }

        public string AccomodationId { get; set; }

        public decimal PaidFees { get; set; }

        public static string CreateCode(int userId, int meetupId)
        {
            string code = ($"{userId:0000}" +
                                         $"-{DateTime.Now.Month}{DateTime.Now.Day}-{meetupId:000}");
            // https://csharp.2000things.com/2011/08/18/392-reversing-a-string-using-the-reverse-method/
            return new string(code.Reverse().ToArray()); ;
        }

        public ERegistrationStatus RegistrationStatus { get; set; }

        [StringLength(5000)]
        public string Notes { get; set; }
    }
}

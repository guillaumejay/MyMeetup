using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Entities
{
    [Table("MeetupAdmins")]
   public class MeetupAdmin
    {
        [Key(),Column(Order = 0)]
        [ForeignKey(nameof (User))]
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public MyMeetupUser User { get; set; }

        [Key(), Column(Order = 1)]
        [ForeignKey(nameof(Meetup))]
        public int MeetupId { get; set; }

        [ForeignKey(nameof(MeetupId))]
        public Meetup Meetup { get; set; }
    }
}

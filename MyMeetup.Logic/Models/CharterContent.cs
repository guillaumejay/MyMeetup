using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMeetUp.Logic.Models
{
    [Table("ContenusChartes")]
    public class CharterContent:EntityWithDate
    {
        public CharterContent()
        {
            IsActive = true;
        }
        [StringLength(80)]
        public string Category { get; set; }

        public string Content { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }

        [ForeignKey(nameof(Meetup))]
        public int? MeetupId { get; set; }

        [ForeignKey(nameof(MeetupId))]
        public virtual Meetup Meetup { get; set; }

        public int Position { get; set; }
    }
}

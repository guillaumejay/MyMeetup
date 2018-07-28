using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace MyMeetUp.Logic.Entities
{
    [Table("CharterContents")]
    [DebuggerDisplay("{Id} {MeetupId} {Category} {Content}")]
    public class CharterContent:EntityWithDate
    {
        public CharterContent()
        {
            IsActive = true;
        }
        [StringLength(80)][Required]
        public string Category { get; set; }

        [Required]
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

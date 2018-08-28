using System.ComponentModel.DataAnnotations;

namespace MyMeetUp.Logic.Models
{
    public class SigninMeetupModel
    {
        [Required][StringLength(60)]
        public string Name
        {
            get; set; 
        }
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required][EmailAddress]
        public string Email { get; set; }

        [Required][StringLength(30)]
        public string PhoneNumber { get; set; }

        public int? MeetupId { get; set; }

        public bool IsOkToGetMeetupsInfo { get; set; }
    }
}

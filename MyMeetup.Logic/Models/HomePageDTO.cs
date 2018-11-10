using System.ComponentModel.DataAnnotations;

namespace MyMeetUp.Logic.Models
{
    public class HomePageDTO
    {
        public string HomeImage { get; set; }

        [Required]
        [StringLength(120)]
        public string HomeTitle { get; set; }

        public string HomeContent { get; set; }
    }
}

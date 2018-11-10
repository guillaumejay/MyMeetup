using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMeetUp.Logic.Entities
{
    [Table("AppParameters")]
  public  class AppParameter:EntityWithDate
    {
        [Required][StringLength(80,MinimumLength = 3)]
        public string Title { get; set; }

        [StringLength(120)]
        public string HomeTitle { get; set; }

        public string HomeContent { get; set; }

        public string HomeImage { get; set; }
    }
}

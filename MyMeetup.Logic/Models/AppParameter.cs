using System.ComponentModel.DataAnnotations;

namespace MyMeetUp.Logic.Models
{
  public  class AppParameter:EntityWithDate
    {
        [Required][StringLength(80,MinimumLength = 3)]
        public string Title { get; set; }
    }
}

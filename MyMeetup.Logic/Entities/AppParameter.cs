using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyMeetUp.Logic.Entities
{
    [Table("AppParameters")]
  public  class AppParameter:EntityWithDate
    {
        [Required][StringLength(80,MinimumLength = 3)]
        public string Title { get; set; }
    }
}

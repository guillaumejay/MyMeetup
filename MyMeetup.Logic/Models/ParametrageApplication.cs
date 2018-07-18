using System.ComponentModel.DataAnnotations;

namespace MyMeetUp.Logic.Modeles
{
  public  class ParametrageApplication:EntityWithDate
    {
        [Required][StringLength(80,MinimumLength = 3)]
        public string Titre { get; set; }
    }
}

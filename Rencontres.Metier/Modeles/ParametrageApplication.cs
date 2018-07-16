using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Rencontres.Metier.Modeles
{
  public  class ParametrageApplication:EntiteDatee
    {
        [Required][StringLength(80,MinimumLength = 3)]
        public string Titre { get; set; }
    }
}

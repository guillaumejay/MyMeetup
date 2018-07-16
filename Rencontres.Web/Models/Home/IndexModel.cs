using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rencontres.Metier.Modeles;

namespace Rencontres.Web.Models.Home
{
    public class IndexModel
    {

        public Rencontre Rencontre { get; set; }

        public List<ContenuCharte> Charte { get; set; }=new List<ContenuCharte>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyMeetUp.Logic.Modeles;

namespace Rencontres.Web.Models.Home
{
    public class IndexModel
    {

        public Rencontre Rencontre { get; set; }

        public List<ContenuCharte> Charter { get; set; }=new List<ContenuCharte>();
    }
}

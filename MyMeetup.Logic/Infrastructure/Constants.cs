using System.Collections.Generic;

namespace MyMeetUp.Logic.Infrastructure
{
    public static class Constants
    {
        public static List<(string text, string id)> InitAccomodation()
        {
            List<(string, string)> accomodations = new List<(string, string)>();
            accomodations.Add(("Emplacement Camping", "Camping"));
            accomodations.Add(("Landette 4 couchages", "Landette4"));
            accomodations.Add(("Landette 6 couchages", "Landette6"));
            accomodations.Add(("Chalet", "Chalet"));
       //     accomodations.Add(("Mobilhome Docinela", "Docinela"));
         //   accomodations.Add(("Mobilhome Pichonela", "Pichonela"));
            accomodations.Add(("Mobilhome Hacienda", "Hacienda"));
            accomodations.Add(("Yourte", "Yourte"));
            accomodations.Add(("Gîte T4", "GiteT4"));
            accomodations.Add(("Chambre dans Gîte T6", "GiteT6"));
            return accomodations;
        }
    }
}

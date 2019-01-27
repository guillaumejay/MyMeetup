using System.Collections.Generic;

namespace MyMeetUp.Logic.Infrastructure
{
    public static class Constants
    {
        public static List<(string text, string id)> InitAccomodation()
        {
            List<(string, string)> accomodations = new List<(string, string)>();
            accomodations.Add(("Emplacement Camping", "Camping"));
            accomodations.Add(("Landette 4 couchages (165€)", "Landette4"));
            accomodations.Add(("Landette 6 couchages (165€)", "Landette6"));
            accomodations.Add(("Chalet (3-4 places, 280€)", "Chalet"));
       //     accomodations.Add(("Mobilhome Docinela", "Docinela"));
         //   accomodations.Add(("Mobilhome Pichonela", "Pichonela"));
            accomodations.Add(("Mobilhome Hacienda (8 places, 340€)", "Hacienda"));
            accomodations.Add(("Yourte (4-6 places, 340 €)", "Yourte"));
            accomodations.Add(("Gîte T4 (8 places, 390€)", "GiteT4"));
            accomodations.Add(("Chambre dans Gîte T6 (212€)", "GiteT6"));
            return accomodations;
        }
    }
}

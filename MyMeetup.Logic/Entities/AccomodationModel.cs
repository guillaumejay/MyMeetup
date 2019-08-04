using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyMeetUp.Logic.Entities
{
public    class AccomodationModel
{
    public AccomodationModel()
        { }

    public AccomodationModel(string id, string description, bool isActive)
    {
        Id = id;
        Description = description;
        IsActive = isActive;
    }
        [Key]
        public string Id { get; set; }

        
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public static List<AccomodationModel> DefaultAccomodationModels
        {
            get
            {
                var accomodations = new List<AccomodationModel>
                {
                    new AccomodationModel("Camping", "Emplacement Camping", true),
                    new AccomodationModel("Docinela", "Mobilhome Docinela (4 places: 2 chambres) 270€", true),
                    new AccomodationModel("Landette1", "Landette 1 couchage (avec sanitaires) (165€)", false),
                    new AccomodationModel("Landette46", "landettes (4 ou 6 places, une seule pièce) 165€", true),
                    new AccomodationModel("ChaletStudios", "Chalets/studio (3-4 places) 280€", true),
                    new AccomodationModel("Hacienda", "Mobilhome Hacienda (8 places, 340€)", true),
                    new AccomodationModel("Yourte", "Yourte (4-6 places / 2 chambres : 340€)", true),
                    new AccomodationModel("GiteT4", "Gîte T4 (8 places, 390€)", true),
                    new AccomodationModel("GiteT6", "Chambre (2-3 lites)( avec douche/wc privés) dans Gîte T6 (212€)", true)
                };

                return accomodations;
            }
        }
    }
}

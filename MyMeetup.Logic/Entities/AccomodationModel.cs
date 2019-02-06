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
                    new AccomodationModel("Landette4", "Landette 4 couchages (165€)", true),
                    new AccomodationModel("Landette6", "Landette 6 couchages (165€)", true),
                    new AccomodationModel("Chalet", "Chalet (3-4 places, 280€)", true),
                    new AccomodationModel("Hacienda", "Mobilhome Hacienda (8 places, 340€)", false),
                    new AccomodationModel("Yourte", "Yourte (4-6 places / 2 chambres : 340€)", true),
                    new AccomodationModel("GiteT4", "Gîte T4 (8 places, 390€)", false),
                    new AccomodationModel("GiteT6", "Chambre dans Gîte T6 (212€)", true)
                };

                return accomodations;
            }
        }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace MyMeetUp.Logic.Models
{
    public class RegisteredUserModel
    {
        public RegisteredUserModel()
        {
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public int RegistrationId { get; set; }

        public string RegCode { get; set; }

        public string Accomodation { get; set; }

        public int AdultNumber { get; set; }

        public int KidNumber { get; set; }
        

        public DateTime RegisteredOn { get; set; }

        /// <summary>
        /// https://datatables.net/manual/data/orthogonal-data
        /// </summary>
        public string RegisteredOnSort => RegisteredOn.ToString("yyyy/MM/dd HH:mm");
    }
}

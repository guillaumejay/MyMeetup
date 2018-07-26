using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        public DateTime RegisteredOn { get; set; }
    }
}

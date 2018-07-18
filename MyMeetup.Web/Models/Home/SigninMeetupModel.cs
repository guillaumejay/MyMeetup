using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Rencontres.Web.Models.Home
{
    public class SigninMeetupModel
    {
        [Required][StringLength(60)]
        public string Name
        {
            get; set; 
        }
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }

        [Required][EmailAddress]
        public string Email { get; set; }

        [Required][StringLength(30)]
        public string PhoneNumher { get; set; }
    }
}

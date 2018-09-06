using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyMeetup.Web.Models.Components
{
    public class UserModel
    {
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsOkToGetMeetupsInfo { get; set; }

        public int Id { get; set; }
    }
}

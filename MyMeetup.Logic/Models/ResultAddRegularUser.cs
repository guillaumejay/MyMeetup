using System;
using System.Collections.Generic;
using System.Text;

namespace MyMeetUp.Logic.Models
{
    public class ResultAddRegularUser
    {
        public int? UserId { get; set; }

        public string RegistrationCode { get; set; }
        public bool UserOk => UserId.HasValue && UserId.Value > 0;
    }
}

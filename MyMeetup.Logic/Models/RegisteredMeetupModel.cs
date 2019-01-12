using System;
using System.Collections.Generic;
using System.Text;

namespace MyMeetUp.Logic.Models
{
    public class RegisteredMeetupModel
    {
        public RegisteredMeetupModel()
        {
        }

        public RegisteredMeetupModel(int meetupId, int registrationid, int userId, string meetupTitle, string registrationCode,DateTime registrationDate)
        {
            MeetupId = meetupId;
            Registrationid = registrationid;
            UserId = userId;
            MeetupTitle = meetupTitle;
            RegistrationCode = registrationCode;
            RegisteredOn = registrationDate;
        }

        public int MeetupId { get; set; }

        public int Registrationid { get; set; }

        public int UserId { get; set; }

        public string MeetupTitle { get; set; }

        public string RegistrationCode { get; set; }


        public DateTime RegisteredOn { get; set; }

        /// <summary>
        /// https://datatables.net/manual/data/orthogonal-data
        /// </summary>
        public string RegisteredOnSort => RegisteredOn.ToString("yyyy/MM/dd hh:mm");
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MyMeetUp.Logic.Infrastructure;

namespace MyMeetUp.Logic.Models
{

    public class UserModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public DateTime? RegisteredOn { get; set; }

        public int NbMetupsRegistered { get; set; }

        public UserModel()
        {
        }

        public UserModel(MyMeetupUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Email = user.Email;
            IsOkToGetMeetupsInfo = user.IsOkToGetMeetupsInfo;

        }

        public bool IsOkToGetMeetupsInfo
        { get; set; }
    }
    public class UsersModel
    {
        public List<UserModel> Users { get; set; } = new List<UserModel>();
    }
}

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyMeetUp.Logic.Infrastructure
{
    public class MyMeetupUser:IdentityUser<int>
    {
        [StringLength(60)][Required]
        public string FirstName { get; set; }

        [StringLength(60)][Required]
        public string LastName { get; set; }
    }
    public class MyMeetupRole : IdentityRole<int>
    {
        public MyMeetupRole() { }
        public MyMeetupRole(string name) { Name = name; }

        public const string Administrateur = "Admin";
        public const string Participant = "Participant";
    }

}

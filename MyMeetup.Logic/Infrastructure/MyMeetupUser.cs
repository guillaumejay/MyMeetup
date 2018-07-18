using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using MyMeetUp.Logic.Models;

namespace MyMeetUp.Logic.Infrastructure
{
    public class MyMeetupUser:IdentityUser<int>
    {
        [StringLength(60)][Required]
        public string FirstName { get; set; }

        [StringLength(60)][Required]
        public string LastName { get; set; }

        public string Initials => FirstName.ToLower().Substring(0, 1) + LastName.ToLower().Substring(0, 1);

        internal static int? CreateUser(MyMeetupUser user, string role, string password, UserManager<MyMeetupUser> userManager)
        {
            IdentityResult result = userManager.CreateAsync(user, password).Result;

            if (result.Succeeded)
            {
                userManager.AddToRoleAsync(user, role).Wait();
                user = userManager.FindByNameAsync(user.UserName).Result;
                return user.Id;
            }

            return null;
        }

        public static MyMeetupUser From(SigninMeetupModel model)
        {
            return new MyMeetupUser
            {
                FirstName=model.FirstName.Trim(),
                LastName = model.Name.Trim(),
                Email=model.Email.Trim(),
                PhoneNumber = model.PhoneNumber.Trim(),
                UserName = model.Email.Trim(),
            };
        }
    }
    public class MyMeetupRole : IdentityRole<int>
    {
        public MyMeetupRole() { }
        public MyMeetupRole(string name) { Name = name; }

        public const string Administrateur = "Admin";
        public const string Participant = "Participant";
    }

}

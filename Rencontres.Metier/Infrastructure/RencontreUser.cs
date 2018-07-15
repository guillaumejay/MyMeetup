using Microsoft.AspNetCore.Identity;

namespace Rencontres.Metier.Infrastructure
{
    public class RencontreUser:IdentityUser<int>
    {
    }
    public class RencontreRole : IdentityRole<int>
    {
        public RencontreRole() { }
        public RencontreRole(string name) { Name = name; }

        public const string Administrateur = "Administrateur";
        public const string Participant = "Participant";
    }

}

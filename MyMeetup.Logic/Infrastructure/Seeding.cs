using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using MyMeetUp.Logic.Entities;
using MyMeetUp.Logic.Infrastructure.DataContexts;

namespace MyMeetUp.Logic.Infrastructure
{
   public static class Seeding
    {
        public  static void SeedData(MyMeetupContext context)
        {
            if (context.AppParameters.Any() == false)
            {
                AppParameter ap = new AppParameter { Title="MyMeetup"};
                context.AppParameters.Add(ap);
                context.SaveChanges();
            }
        }

        public static void SeedDataByMigration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meetup>().HasData(new Meetup
            {
                Id = 1,
                Title = "La Taillade 2018",
                StartDate = new DateTime(2018, 10, 22),
                EndDate = new DateTime(2018, 10, 29),
                IsVisible = true,
                OpenForRegistrationOn = new DateTime(2018, 2, 1),
                PublicDescription = "Rencontre près de Casteljaloux(47). Situé dans un écrin de forêt, les hébergements se répartissent entre gîtes, landettes, emplacements pour tentes et camions, et quelques yourtes.<br/>"
                + @"<div>
<h2><u>Comment s'inscrire &agrave; la rencontre ?</u></h2>
</div>
<div>Ne peuvent s'inscrire &agrave; cette rencontre que les personnes qui s'engagent &agrave; respecter la charte mise en place.</div>
<div><strong>Proc&eacute;dure&nbsp;</strong>:</div>
<div>1. Vous lisez l'engagement que vous demande la charte</div>
<div>2. Si la charte vous convient : vous vous engagez &agrave; la respecter en la validant, la signant num&eacute;riquement et en nous donnant vos coordonn&eacute;es : le tout nous sera adress&eacute; directement.</div>",
                RegisteredDescription = "<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail <div><strong>",
                TitleImage = "La-Taillade.jpg"
            });
            List<CharterContent> chartes = new List<CharterContent>
            {
                new CharterContent
                {
    
                    Category = "Communication sur le respect des lieux",
                    Content =
                        "Chaque membre de votre famille, présent à la rencontre, doit être informé que le respect des lieux est important pour que nous puissions revenir. Aussi merci de nous prévenir en cas d’éventuels dégâts pour montrer aux gérants notre implication dans la remise en état des lieux.",
                    Position=1

                },
                new CharterContent
                {
     
                    Category = "Animaux",
                    Content =
                        "Les chiens sont tolérés, à condition qu'ils restent attachés ou auprès de vous en permanence.<br/>Ils ne doivent également pas être bruyants.",
                    Position=3

                },
                new CharterContent
                {
         
                    Category = "Participation financière",
                    Content =
                        " Chaque famille participante devra régler 3€ de participation à Rencontres Nonscos : ces paiements permettront à l'association de couvrir ses dépenses d'existence (assurance notamment)",
                    Position=2

                },
                new CharterContent
                {
             
                    Category = "Alcool",
                    Content =
                        "La consommation d’alcool doit être raisonnée, pour toutes les personnes participantes, quel que soit leur âge, et bien sûr, les parents ou les référents sont invités à être attentifs à cette problématique vis-à-vis des personnes dont ils sont responsables.",
                    Position=4

                },
                new CharterContent
                {
           
                    Category = "Spécifique à la Taillade",
                    Content =
                        "La tradition est née de faire des trous autour du barbecue, il est important de les reboucher au départ des enfants",
                    MeetupId=1,
                    Position=1

                }
            };
            int id = 1;
            foreach (var charte in chartes)
            {
                charte.Id = id++;
                modelBuilder.Entity<CharterContent>().HasData(charte);
            }

        }

        public static void SeedRoles(RoleManager<MyMeetupRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync(MyMeetupRole.Participant).Result)
            {
                var role = new MyMeetupRole(MyMeetupRole.Participant);

                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync(MyMeetupRole.Administrateur).Result)
            {
                var role = new MyMeetupRole(MyMeetupRole.Administrateur);
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public static void SeedUsers(UserManager<MyMeetupUser> userManager)
        {
            if (userManager.FindByNameAsync("admin").Result == null)
            {
                var user = new MyMeetupUser
                {
                    UserName = "admin",
                    Email = "guillaume.jay@gmail.com",
                    FirstName = "Guillaume",
                    LastName = "Jay",
                    LockoutEnabled=false
                };
                string pwd = $"admin{DateTime.Now:yyMMdd}";
                MyMeetupUser.CreateUser(user, MyMeetupRole.Administrateur, pwd, userManager);
            }
         
        }
    }
}

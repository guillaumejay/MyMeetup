using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Entities;

namespace MyMeetUp.Logic.Infrastructure
{
   public static class Seeding
    {
        public static void SeedData(ModelBuilder modelBuilder)
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
<div>2. Si la charte vous convient : vous vous engagez &agrave; la respecter en la validant, la signant num&eacute;riquement et en nous donnant vos coordonn&eacute;es : le tout nous sera adress&eacute; directement.</div>
<div>3. Nous vous confirmons votre pr&eacute;-r&eacute;servation et transmettons au village de vacances de la Taillade votre nom et votre N&deg; de pr&eacute;-r&eacute;servation</div>
<div>4. Vous pouvez alors contacter le village de vacances pour effectuer votre r&eacute;servation aupr&egrave;s d'eux (en leur rappelant votre N&deg; de pr&eacute;-r&eacute;servation).</div>",
                RegisteredDescription = "<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail &agrave; : francois.fonseca@solincite.org</strong></div>\r\n<div>Vous devrez lui indiquer vos&nbsp;<strong>noms/pr&eacute;noms/adresse postale/nb d&rsquo;adultes+d&rsquo;enfants.</strong></div>\r\n<div><strong>Il n&rsquo;y a qu&rsquo;un seul interlocuteur par logement : un logement est r&eacute;serv&eacute; par une seule famille, c&rsquo;est elle qui fait la r&eacute;servation et paiera la somme totale au village de vacances. </strong>Vous pouvez donc r&eacute;server &agrave; votre nom et trouver d&rsquo;autres familles pour partager, gr&acirc;ce au document Pad mis &agrave; disposition <strong>:&nbsp;</strong><strong><a href=\"https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B\">https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B</a></strong></div>\r\n<div>Ce document servira &agrave; partager toutes les infos sur la rencontre (logements, covoiturage, activit&eacute;s,&hellip;)</div>",
                TitleImage = "La-Taillade.jpg"
            });
            modelBuilder.Entity<Meetup>().HasData(new Meetup
            {
                Id = 2,
                Title = "La Taillade Printemps 2019",
                StartDate = new DateTime(2019, 03, 22),
                EndDate = new DateTime(2019, 03, 29),
                IsVisible = false,
                OpenForRegistrationOn = null,
                PublicDescription = "Rencontre près de Casteljaloux(47). Situé dans un écrin de forêt, les hébergements se répartissent entre gîtes, landettes, emplacements pour tentes et camions, et quelques yourtes.",
                RegisteredDescription = "<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail &agrave; : francois.fonseca@solincite.org</strong></div>\r\n<div>Vous devrez lui indiquer vos&nbsp;<strong>noms/pr&eacute;noms/adresse postale/nb d&rsquo;adultes+d&rsquo;enfants.</strong></div>\r\n<div><strong>Il n&rsquo;y a qu&rsquo;un seul interlocuteur par logement : un logement est r&eacute;serv&eacute; par une seule famille, c&rsquo;est elle qui fait la r&eacute;servation et paiera la somme totale au village de vacances. </strong>Vous pouvez donc r&eacute;server &agrave; votre nom et trouver d&rsquo;autres familles pour partager, gr&acirc;ce au document Pad mis &agrave; disposition <strong>:&nbsp;</strong><strong><a href=\"https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B\">https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B</a></strong></div>\r\n<div>Ce document servira &agrave; partager toutes les infos sur la rencontre (logements, covoiturage, activit&eacute;s,&hellip;)</div>",
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

            modelBuilder.Entity<AppParameter>().HasData(
                new AppParameter { Id = 1, Title = "Rencontres Non Scolarisees" });
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
            if (userManager.FindByNameAsync("lorijay").Result == null)
            {
                var user = new MyMeetupUser
                {
                    UserName = "lorijay",
                    Email = "laurie.gj@gmail.com",
                    FirstName = "Lori",
                    LastName = "Jay",
                    LockoutEnabled = false
                };
                string pwd = $"lj{DateTime.Now:yyMMdd}";
                MyMeetupUser.CreateUser(user, MyMeetupRole.Administrateur, pwd, userManager);
            }
            if (userManager.FindByNameAsync("mathildegarioud").Result == null)
            {
                var user = new MyMeetupUser
                {
                    UserName = "mathildegarioud",
                    Email = "mathilde.garioud@riseup.net",
                    FirstName = "Mathilde",
                    LastName = "Garioud",
                    LockoutEnabled = false
                };
                string pwd = $"mg{DateTime.Now:yyMMdd}";
                MyMeetupUser.CreateUser(user, MyMeetupRole.Administrateur, pwd, userManager);
            }
            // mathilde.garioud@riseup.net>
            // samara@joy.lautre.net
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Models;

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
                PublicDescription = "Rencontre près de Casteljaloux(47) du 22 au 29 octobre 2018. Situé dans un écrin de forêt, les hébergements se répartissent entre gîtes, landettes, emplacements pour tentes et camions, et quelques yourtes.",
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
                PublicDescription = "Rencontre près de Casteljaloux(47) du 22 au 29 octobre 2018. Situé dans un écrin de forêt, les hébergements se répartissent entre gîtes, landettes, emplacements pour tentes et camions, et quelques yourtes.",
                RegisteredDescription = "<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail &agrave; : francois.fonseca@solincite.org</strong></div>\r\n<div>Vous devrez lui indiquer vos&nbsp;<strong>noms/pr&eacute;noms/adresse postale/nb d&rsquo;adultes+d&rsquo;enfants.</strong></div>\r\n<div><strong>Il n&rsquo;y a qu&rsquo;un seul interlocuteur par logement : un logement est r&eacute;serv&eacute; par une seule famille, c&rsquo;est elle qui fait la r&eacute;servation et paiera la somme totale au village de vacances. </strong>Vous pouvez donc r&eacute;server &agrave; votre nom et trouver d&rsquo;autres familles pour partager, gr&acirc;ce au document Pad mis &agrave; disposition <strong>:&nbsp;</strong><strong><a href=\"https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B\">https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B</a></strong></div>\r\n<div>Ce document servira &agrave; partager toutes les infos sur la rencontre (logements, covoiturage, activit&eacute;s,&hellip;)</div>",
                TitleImage = "La-Taillade.jpg"
            });
            List<CharterContent> chartes = new List<CharterContent>
            {
                new CharterContent
                {
                    Id = 1,
                    Category = "Animaux",
                    Content =
                        "<ul><li>Les chiens sont tolérés, à condition qu'ils restent attachés ou auprès de vous en permanence.</li><li>Ils ne doivent également pas être bruyants.</li></ul>",
                    Position=1

                },
                new CharterContent
                {
                    Id = 2,
                    Category = "Alcool",
                    Content =
                        "<ul><li>La consommation d’alcool doit être raisonnée, pour toutes les personnes participantes, quel que soit leur âge, et bien sûr, les parents ou les référents sont invités à être attentifs à cette problématique vis-à-vis des personnes dont ils sont responsables.</li></ul>",
                    Position=2

                },
                new CharterContent
                {
                    Id = 3,
                    Category = "Spécifique à la Taillade",
                    Content =
                        "<ul><li>La tradition est née de faire des trous autour du barbecue, il est important de les reboucher au départ des enfants</li></ul>",
                    MeetupId=1,
                    Position=1

                }
            };

            foreach (var charte in chartes)
                modelBuilder.Entity<CharterContent>().HasData(charte);

            modelBuilder.Entity<AppParameter>().HasData(
                new AppParameter { Id = 1, Title = "Rencontres Non Scolarisees" });
        }
    }
}
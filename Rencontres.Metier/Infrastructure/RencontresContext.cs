using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rencontres.Metier.Modeles;

namespace Rencontres.Metier.Infrastructure
{
    public class RencontresContext : IdentityDbContext<RencontreUser, RencontreRole, int>
    {
        public RencontresContext(DbContextOptions<RencontresContext> options) : base(options)
        {
        }

        public DbSet<ContenuCharte> ContenusChartes { get; set; }
        public DbSet<Inscription> Inscriptions { get; set; }

        public DbSet<Rencontre> Rencontres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ResponsableRencontre>()
                .HasKey(c => new { c.UserId, c.RencontreId });
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(EntiteDatee).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property("CreationLe")
                    .HasDefaultValueSql("getutcdate()");
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property("ModificationLe")
                    .HasDefaultValueSql("getutcdate()");
            }
            InitialiserDonnees(modelBuilder);
        }

        private void InitialiserDonnees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rencontre>().HasData(new Rencontre
            {
                Id = 1,
                Titre = "La Taillade 2018",
                DateDebut = new DateTime(2018, 10, 22),
                DateFin = new DateTime(2018, 10, 29),
                EstVisible = true,
                OuvertInscriptionLe = new DateTime(2018, 1, 1),
                DescriptionPublique= "Rencontre près de Casteljaloux(47) du 22 au 29 octobre 2018.",
                DescriptionInscrit = "<div><strong>Toutes les inscriptions (locatif ou camping) doivent se faire uniquement par mail &agrave; : francois.fonseca@solincite.org</strong></div>\r\n<div>Vous devrez lui indiquer vos&nbsp;<strong>noms/pr&eacute;noms/adresse postale/nb d&rsquo;adultes+d&rsquo;enfants.</strong></div>\r\n<div><strong>Il n&rsquo;y a qu&rsquo;un seul interlocuteur par logement : un logement est r&eacute;serv&eacute; par une seule famille, c&rsquo;est elle qui fait la r&eacute;servation et paiera la somme totale au village de vacances. </strong>Vous pouvez donc r&eacute;server &agrave; votre nom et trouver d&rsquo;autres familles pour partager, gr&acirc;ce au document Pad mis &agrave; disposition <strong>:&nbsp;</strong><strong><a href=\"https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B\">https://semestriel.framapad.org/p/LaTaillade_qfmnV6VC4B</a></strong></div>\r\n<div>Ce document servira &agrave; partager toutes les infos sur la rencontre (logements, covoiturage, activit&eacute;s,&hellip;)</div>",
                ImageTitre="La-Taillade.jpg"

            });
            List<ContenuCharte> chartes = new List<ContenuCharte>
            {
                new ContenuCharte
                {
                    Id = 1,
                    Categorie = "Animaux",
                    Contenu =
                        "<ul><li>Les chiens sont tolérés, à condition qu'ils restent attachés ou auprès de vous en permanence.</li><li>Ils ne doivent également pas être bruyants.</li></ul>",

                },
                new ContenuCharte
                {
                    Id = 2,
                    Categorie = "Spécifique à la Taillade",
                    Contenu =
                        "<ul><li>La tradition est né de faire des trous autour du barbecue, il est important de les reboucher au départ des enfants</li></ul>",
                    RencontreId=1
                }
            };

            foreach (var charte in chartes)
                modelBuilder.Entity<ContenuCharte>().HasData(charte);

            modelBuilder.Entity<ParametrageApplication>().HasData(
                new ParametrageApplication {Id = 1, Titre = "Rencontres Non Scolarisees"});
        }
    }

}

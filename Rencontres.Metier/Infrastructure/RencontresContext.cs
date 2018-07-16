using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Rencontres.Metier.Modeles;

namespace Rencontres.Metier.Infrastructure
{
   public class RencontresContext: IdentityDbContext<RencontreUser, RencontreRole, int>
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
        }
    }

}

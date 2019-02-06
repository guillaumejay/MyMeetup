using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Entities;

namespace MyMeetUp.Logic.Infrastructure.DataContexts
{
    public abstract class MyMeetupContext : IdentityDbContext<MyMeetupUser, MyMeetupRole, int>
    {
        protected MyMeetupContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppParameter> AppParameters { get; set; }

        public DbSet<CharterContent> CharterContents { get; set; }
        public DbSet<Registration> Registrations { get; set; }

        public DbSet<Meetup> Meetups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<MeetupAdmin>()
                .HasKey(c => new { c.UserId, c.MeetupId });
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()
                .Where(e => typeof(EntityWithDate).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(EntityWithDate.CreatedAt))
                    .HasDefaultValueSql("getutcdate()");
                modelBuilder
                    .Entity(entityType.ClrType)
                    .Property(nameof(EntityWithDate.UpdatedAt))
                    .HasDefaultValueSql("getutcdate()");
            }

        }

 
    }

}

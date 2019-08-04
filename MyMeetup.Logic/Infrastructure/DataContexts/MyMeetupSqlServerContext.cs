using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Entities;

namespace MyMeetUp.Logic.Infrastructure.DataContexts
{
    //Add-Migration InitialCreate -Context MyMeetupSqlServerContext -OutputDir Migrations\SqlServerMigrations
    public class MyMeetupSqlServerContext:MyMeetupContext
    {
        public MyMeetupSqlServerContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
       //     modelBuilder.Entity<Payment>().Property(x => x.PaymentDate).HasColumnType("datetime2");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyMeetUp.Logic.Infrastructure.DataContexts
{
    //Add-Migration InitialCreate -Context MyMeetupSqlLiteContext -OutputDir Migrations\SqliteMigrations
    public class MeetupContextSqlLiteFactory : IDesignTimeDbContextFactory<MyMeetupSqlLiteContext>
    {
        public MyMeetupSqlLiteContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyMeetupContext>();
            optionsBuilder.UseSqlite(
                "Data Source=MyMeetupLocal.db;");

            return new MyMeetupSqlLiteContext(optionsBuilder.Options);
        }
    }
}

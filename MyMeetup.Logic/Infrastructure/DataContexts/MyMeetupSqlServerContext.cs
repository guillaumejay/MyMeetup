using Microsoft.EntityFrameworkCore;

namespace MyMeetUp.Logic.Infrastructure.DataContexts
{
    //Add-Migration InitialCreate -Context MyMeetupSqlServerContext -OutputDir Migrations\SqlServerMigrations
    public class MyMeetupSqlServerContext:MyMeetupContext
    {
        public MyMeetupSqlServerContext(DbContextOptions options) : base(options)
        {
        }
    }
}

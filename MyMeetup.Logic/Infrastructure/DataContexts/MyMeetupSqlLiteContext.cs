using Microsoft.EntityFrameworkCore;

namespace MyMeetUp.Logic.Infrastructure.DataContexts
{
    /// <summary>
    ///  Add-Migration InitialCreate -Context MyMeetupSqlLiteContext -OutputDir Migrations\SqlLiteMigrations
    /// </summary>
    public class MyMeetupSqlLiteContext:MyMeetupContext
    {
        //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/provider
        public MyMeetupSqlLiteContext(DbContextOptions options) : base(options)
        {
        }
    }
}

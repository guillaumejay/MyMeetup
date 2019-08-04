using Microsoft.EntityFrameworkCore;

namespace MyMeetUp.Logic.Infrastructure.DataContexts
{
    /// <summary>
    ///  Add-Migration  -Context MyMeetupSqliteContext -OutputDir Migrations\SqliteMigrations
    /// </summary>
    public class MyMeetupSqliteContext:MyMeetupContext
    {
        //https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/provider
        public MyMeetupSqliteContext(DbContextOptions options) : base(options)
        {
        }
    }
}

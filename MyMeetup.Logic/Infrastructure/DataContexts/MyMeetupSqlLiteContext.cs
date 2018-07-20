using Microsoft.EntityFrameworkCore;
using MyMeetUp.Logic.Infrastructure.DataContexts;

namespace MyMeetUp.Logic.Infrastructure
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

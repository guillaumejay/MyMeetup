using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyMeetUp.Logic.Infrastructure.DataContexts
{

    public class MeetupContextFactory : IDesignTimeDbContextFactory<MyMeetupSqlServerContext>
    {
        public MyMeetupSqlServerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyMeetupContext>();
            optionsBuilder.UseSqlServer(
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MyMeetupLocal;Integrated Security=SSPI;");

            return new MyMeetupSqlServerContext(optionsBuilder.Options);
        }
    }
}

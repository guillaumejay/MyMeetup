using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MyMeetUp.Logic.Infrastructure
{

    public class MeetupContextFactory : IDesignTimeDbContextFactory<MyMeetupContext>
    {
        public MyMeetupContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MyMeetupContext>();
            optionsBuilder.UseSqlServer(
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MyMeetupLocal;Integrated Security=SSPI;");

            return new MyMeetupContext(optionsBuilder.Options);
        }
    }
}

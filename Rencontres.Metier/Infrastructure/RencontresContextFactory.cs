using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Rencontres.Metier.Infrastructure
{

    public class RencontresContextFactory : IDesignTimeDbContextFactory<RencontresContext>
    {
        public RencontresContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RencontresContext>();
            optionsBuilder.UseSqlServer(
                "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=RencontresLocal;Integrated Security=SSPI;");

            return new RencontresContext(optionsBuilder.Options);
        }
    }
}

using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using ContosoPizza.Data;

namespace ContosoPizza.Areas.Identity.Data
{
    public class ContosoPizzaContextFactory : IDesignTimeDbContextFactory<ContosoPizzaContext>
    {
        public ContosoPizzaContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ContosoPizzaContext>();
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ContosoPizza;Trusted_Connection=True;MultipleActiveResultSets=true");

            return new ContosoPizzaContext(optionsBuilder.Options);
        }
    }
}

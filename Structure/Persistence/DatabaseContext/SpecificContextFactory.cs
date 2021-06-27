using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.DatabaseContext
{
    /// <summary>
    /// Factory class that handles migrations in class library 
    /// added for SpecificContext database 
    /// </summary>
    public partial class SpecificContextFactory : IDesignTimeDbContextFactory<SpecificContext>
    {
        public SpecificContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SpecificContext>();
            builder.UseSqlServer(ConnectionStrings.GetSpecificContextConnection());
            return new SpecificContext(builder.Options);
        }
    }
}
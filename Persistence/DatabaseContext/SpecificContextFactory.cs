using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Persistence.DatabaseContext
{
    public partial class SpecificContextFactory : IDesignTimeDbContextFactory<SpecificContext>
    {
        public SpecificContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SpecificContext>();
            builder.UseSqlServer(new ConnectionStrings().GetSpecificContextConnection());
            return new SpecificContext(builder.Options);
        }
    }
}
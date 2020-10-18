using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Persistence.DatabaseContext
{
    public class SpecificContextFactory : IDesignTimeDbContextFactory<SpecificContext>
    {
        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        public SpecificContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<SpecificContext>();
            builder.UseSqlServer(Configuration.GetConnectionString("SpecificContext"));
            return new SpecificContext(builder.Options);
        }
    }
}
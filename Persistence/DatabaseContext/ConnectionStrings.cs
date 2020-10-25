using System.IO;
using Microsoft.Extensions.Configuration;

namespace Persistence.DatabaseContext
{

    // Configure or gets  all database connection strings 
    public partial class ConnectionStrings
    {
        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();


        // get connection string from appsettings.json for SpecificContext database context
        public string GetSpecificContextConnection() => Configuration.GetConnectionString("SpecificContext");
    }
}
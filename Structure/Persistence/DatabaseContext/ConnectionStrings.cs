using Microsoft.Extensions.Configuration;
using System.IO;

namespace Persistence.DatabaseContext
{

    // Configure or gets  all database connection strings 
    public static partial class ConnectionStrings
    {
        private static IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();


        // get connection string from appsettings.json for SpecificContext database context
        public static string GetSpecificContextConnection() => Configuration.GetConnectionString("SpecificContext");
    }
}
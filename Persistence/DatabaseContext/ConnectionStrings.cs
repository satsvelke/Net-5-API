using System.IO;
using Microsoft.Extensions.Configuration;

namespace Persistence.DatabaseContext
{
    public class ConnectionStrings
    {
        private IConfiguration Configuration => new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
           .AddJsonFile("appsettings.json")
           .Build();
        public string GetSpecificContextConnection()
        {
            return Configuration.GetConnectionString("SpecificContext");
        }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
    }
}
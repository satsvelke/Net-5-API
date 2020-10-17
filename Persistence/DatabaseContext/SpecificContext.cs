using IPersistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext
{
    // SpecificContext named for initial setup 
    public partial class SpecificContext : DbContext, ISpecificContext
    {
        public SpecificContext(DbContextOptions<SpecificContext> options) : base(options)
        {

        }
    }
}
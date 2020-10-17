using IPersistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext
{
    // SpecificContext is ef database cotext , named for initial setup 
    public partial class SpecificContext : DbContext, ISpecificContext
    {
        public SpecificContext(DbContextOptions<SpecificContext> options) : base(options)
        {

        }
    }
}
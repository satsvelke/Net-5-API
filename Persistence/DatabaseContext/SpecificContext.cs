using Microsoft.EntityFrameworkCore;
using Model;

namespace Persistence.DatabaseContext
{
    // SpecificContext is ef database cotext , named for initial setup 
    public partial class SpecificContext : DbContext
    {
        public SpecificContext(DbContextOptions<SpecificContext> options) : base(options)
        {
        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Exception> Exceptions { get; set; }
    }
}
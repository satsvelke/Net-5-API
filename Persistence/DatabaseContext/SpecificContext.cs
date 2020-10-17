using Microsoft.EntityFrameworkCore;
using Model;
using Persistence.Interface;

namespace Persistence.DatabaseContext
{
    // SpecificContext is ef database cotext , named for initial setup 
    public partial class SpecificContext : DbContext
    {
        public SpecificContext(DbContextOptions<SpecificContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
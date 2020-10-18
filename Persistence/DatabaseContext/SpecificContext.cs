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
        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     if (!optionsBuilder.IsConfigured)
        //     {
        //         optionsBuilder.UseSqlServer(new ConnectionStrings().GetSpecificContextConnection());
        //     }
        // }

        public virtual DbSet<User> Users { get; set; }
    }
}
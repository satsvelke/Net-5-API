using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Interface;
using Persistence.Repository;
using Persistence.UnitOfWork;

namespace Persistence.Dependency
{
    public static partial class RegisterDependency
    {
        public static void GetPersistenceDependency(this IServiceCollection service)
        {
            // register DbContext
            service.AddDbContext<SpecificContext>(o => o.UseSqlServer(new ConnectionStrings().GetSpecificContextConnection()));

            // register Unitof work repository by 
            service.AddScoped<IWork, Work>();

            //persistance layer dependency
            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
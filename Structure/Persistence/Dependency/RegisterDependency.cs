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
            service.AddDbContext<SpecificContext>(o => o.UseSqlServer(ConnectionStrings.GetSpecificContextConnection()));

            // register work to Iwork   
            service.AddScoped<IWork, Work>();

            //persistance layer dependency (all repositories)
            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
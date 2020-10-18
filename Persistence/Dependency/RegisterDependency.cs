using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Interface;
using Persistence.Repository;

namespace Persistence.Dependency
{
    public static partial class RegisterDependency
    {
        public static void GetPersistenceDependency(this IServiceCollection service)
        {
            // register DbContext
            service.AddDbContext<SpecificContext>(o => o.UseSqlServer(new ConnectionStrings().GetSpecificContextConnection()));

            service.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
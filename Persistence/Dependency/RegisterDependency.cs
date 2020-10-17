using IPersistence;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;

namespace Persistence.Dependency
{
    public static class RegisterDependency
    {
        public static void GetDependency(this IServiceCollection service)
        {
            service.AddScoped<ISpecificContext, SpecificContext>();
        }
    }
}
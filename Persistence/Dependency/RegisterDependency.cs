using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Interface;

namespace Persistence.Dependency
{
    public static class RegisterDependency
    {
        public static void GetDependency(this IServiceCollection service)
        {
            // SpecificContext is ef Database context, name can be changed according to database
            service.AddScoped<ISpecificContext, SpecificContext>();
        }
    }
}
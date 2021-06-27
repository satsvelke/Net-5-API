using AutoMapper;
using Workflow.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Workflow.Depenedency
{
    public static partial class RegisterDependency
    {
        // mapped all dependencies from Workflow 
        public static void GetBusinessDependency(this IServiceCollection service)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapping());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            service.AddSingleton(mapper);

            // business layer dependency
            service.AddScoped<IUserLogic, UserLogic>();

        }
    }
}
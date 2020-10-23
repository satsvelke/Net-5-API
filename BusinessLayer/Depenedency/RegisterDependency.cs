using AutoMapper;
using BusinessLayer.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessLayer.Depenedency
{
    public static partial class RegisterDependency
    {
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
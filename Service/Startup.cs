using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using Persistence.DatabaseContext;

namespace Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // This is the service to allow cors 
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
               {
                   builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                          .AllowAnyHeader();
               }));

            // this allows the response to default json format
            services.AddControllers().AddNewtonsoftJson(options =>
                       {
                           options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                           options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                       }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // add your database connecttion 
            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DatabaseContext")));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // uncommet to use https 
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

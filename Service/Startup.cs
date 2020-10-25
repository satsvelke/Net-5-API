using System.Linq;
using System.Text;
using BusinessLayer.AppSettings;
using BusinessLayer.Depenedency;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Middleware;
using Newtonsoft.Json.Serialization;
using Persistence.Dependency;
using ViewModel;

namespace Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // allows cors 
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
               {
                   builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                          .AllowAnyHeader();
               }));

            services.AddControllers();

            // allows json output 
            services.AddControllers().AddNewtonsoftJson(options =>
                       {
                           options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                           options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                           options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                       }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            ///jwt configuration 
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["JwtSettings:Issuer"],
                    ValidAudience = Configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SecretKey"])),
                };
            });

            // read the jwt configurtation from  appsettings.json - section JwtSettings
            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            // read default ErrorMessages from appsettings.json -- Section DefaultMessage
            services.Configure<DefaultMessage>(Configuration.GetSection("DefaultMessage"));

            // encryption settings from appsettings.json - section EncryptionSettings
            services.Configure<EncryptionSettings>(Configuration.GetSection("EncryptionSettings"));

            // get all dependency from persistance layer 
            services.GetPersistenceDependency();

            // get dependency from BusinessLayer
            services.GetBusinessDependency();

            // swagger configuration 
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            // encryption/decryption configuration 
            services.AddDataProtection();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            // custom excepetionHandler middleware 
            app.UseExceptionHandler(err => err.UseExceptions(env));

            // app.UseDeveloperExceptionPage();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });

            // if (env.IsDevelopment())
            //     app.UseDeveloperExceptionPage();

            // uncommet to use https 
            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

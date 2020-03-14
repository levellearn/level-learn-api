using AutoMapper;
using LevelLearn.Domain.Services.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.UnityOfWorks;
using LevelLearn.Service.Services.Institucional;
using LevelLearn.WebApi.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LevelLearn.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //var builder = new ConfigurationBuilder()
            //   .SetBasePath(env.ContentRootPath)
            //   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            //   .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
            //   .AddEnvironmentVariables();
            //this.Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(c =>
            {
                c.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(o => { 
                o.JsonSerializerOptions.IgnoreNullValues = true; 
            });

            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // DBContext
            var connectionString = Configuration.GetConnectionString("SQLServerConnection");
            services.AddDbContext<LevelLearnContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business Services
            services.AddTransient<IInstituicaoService, InstituicaoService>();


            // CORS
            //services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            //app.UseCors(option => option.AllowAnyOrigin());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}

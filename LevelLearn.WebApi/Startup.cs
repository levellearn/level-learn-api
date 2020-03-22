using AutoMapper;
using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.UnityOfWorks;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Services.Institucional;
using LevelLearn.Service.Services.Usuarios;
using LevelLearn.WebApi.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace LevelLearn.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;          
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // CORS
            services.AddCors();

            services.AddControllers(c =>
            {
                c.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.IgnoreNullValues = true;
            });

            services.Configure<JWTSettings>(Configuration.GetSection("JWTSettings"));
            var jwtSettings = Configuration.GetSection("JWTSettings").Get<JWTSettings>();

            // DBContext
            ConfigureDbContexts(services);

            // Identity
            ConfigureIdentity(services);

            // JWT
            ConfigureJWTAuthentication(services, jwtSettings);

            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // UnitOfWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Business Services
            ConfigureBusinessServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            LevelLearnContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Criação de estruturas, usuários e permissões Identity
            new IdentityInitializer(context, userManager, roleManager).Initialize();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option => option.AllowAnyOrigin());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<LevelLearnContext>()
                        .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

                // User settings
                options.User.RequireUniqueEmail = true;
            });
        }

        private void ConfigureDbContexts(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SQLServerConnection");

            services.AddDbContext<LevelLearnContext>(opt =>
            {
                opt.UseSqlServer(connectionString);
            });
        }

        private void ConfigureBusinessServices(IServiceCollection services)
        {
            services.AddTransient<IInstituicaoService, InstituicaoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<ITokenService, TokenService>();
        }

        private void ConfigureJWTAuthentication(IServiceCollection services, JWTSettings jwtSettings)
        {
            var key = Encoding.ASCII.GetBytes(jwtSettings.ChavePrivada);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        Debug.WriteLine("Token válido" + context.SecurityToken);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Debug.WriteLine("Token inválido" + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = true; //TODO: Ajustar HTTPS 
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = jwtSettings.ValidoEm,
                    ValidIssuer = jwtSettings.Emissor,
                    ValidateLifetime = true, 
                    ClockSkew = TimeSpan.FromSeconds(30)
                };
            });

            // Ativa o uso do token como forma de autorizar o acesso
            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme‌​)
                    .RequireAuthenticatedUser().Build());
            });

        }


    }
}

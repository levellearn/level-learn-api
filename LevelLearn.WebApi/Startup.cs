using AutoMapper;
using LevelLearn.Domain.Entities.AppSettings;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Institucional;
using LevelLearn.Infra.EFCore.Contexts;
using LevelLearn.Infra.EFCore.UnityOfWorks;
using LevelLearn.Resource;
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
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO.Compression;
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

            // GZip
            ConfigureGZipCompression(services);

            // Culture Localization
            services.AddLocalization();
            services.AddScoped<ISharedResource, SharedResource>();

            services.AddControllers(c =>
            {
                c.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.IgnoreNullValues = true;
            });

            //services.AddHttpContextAccessor();

            // App Settings
            services.Configure<AppSettings>(Configuration);

            // Redis Cache
            ConfigureRedis(services);

            // DB Context
            ConfigureDbContexts(services);

            // Identity
            ConfigureIdentity(services);

            // JWT
            ConfigureJWTAuthentication(services);

            // Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            // Unit of Work
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

            #region Culture Localization

            var culturasSuportadas = new[]
            {
                new CultureInfo("pt-BR"),
                new CultureInfo("en-US")
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = culturasSuportadas,
                SupportedUICultures = culturasSuportadas
            });

            #endregion

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

        private static void ConfigureGZipCompression(IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            })
            .AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });
        }

        private void ConfigureIdentity(IServiceCollection services)
        {
            var appSettings = Configuration.Get<AppSettings>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddRoles<IdentityRole>()
                        .AddEntityFrameworkStores<LevelLearnContext>()
                        .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = RegraAtributo.Pessoa.SENHA_REQUER_DIGITO;
                options.Password.RequiredLength = RegraAtributo.Pessoa.SENHA_TAMANHO_MIN;
                options.Password.RequireNonAlphanumeric = RegraAtributo.Pessoa.SENHA_REQUER_ESPECIAL;
                options.Password.RequireUppercase = RegraAtributo.Pessoa.SENHA_REQUER_MAIUSCULO;
                options.Password.RequireLowercase = RegraAtributo.Pessoa.SENHA_REQUER_MINUSCULO;

                // Lockout settings
                options.Lockout.MaxFailedAccessAttempts = appSettings.IdentitySettings.TentativaMaximaAcesso;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(appSettings.IdentitySettings.TempoBloqueioMinutos);

                // User settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });
        }

        private void ConfigureRedis(IServiceCollection services)
        {
            var appSettings = Configuration.Get<AppSettings>();
            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = appSettings.ConnectionStrings.RedisCache;
                options.InstanceName = appSettings.ConnectionStrings.RedisInstanceName;
            });
        }

        private void ConfigureDbContexts(IServiceCollection services)
        {
            //var connectionString = Configuration.GetConnectionString("LevelLearnSQLServer");
            var appSettings = Configuration.Get<AppSettings>();

            services.AddDbContext<LevelLearnContext>(opt =>
            {
                opt.UseSqlServer(appSettings.ConnectionStrings.LevelLearnSQLServer);
            });
        }

        private void ConfigureBusinessServices(IServiceCollection services)
        {
            services.AddTransient<IInstituicaoService, InstituicaoService>();
            services.AddTransient<IUsuarioService, UsuarioService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailService, EmailService>();
        }

        private void ConfigureJWTAuthentication(IServiceCollection services)
        {
            var sp = services.BuildServiceProvider();
            var redisCache = sp.GetService<IDistributedCache>();

            var appSettings = Configuration.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.JWTSettings.ChavePrivada);

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
                        return ValidateToken(context, redisCache);
                    },
                    OnAuthenticationFailed = context =>
                    {
                        Debug.WriteLine("Token inválido: " + context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = appSettings.JWTSettings.ValidoEm,
                    ValidIssuer = appSettings.JWTSettings.Emissor,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromSeconds(appSettings.JWTSettings.TempoToleranciaSegundos)
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

        /// <summary>
        /// Verifica se o token está armazando no BD de cache 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="redisCache"></param>
        /// <returns></returns>
        private Task ValidateToken(TokenValidatedContext context, IDistributedCache redisCache)
        {
            var jwtId = context.SecurityToken.Id;
            var value = redisCache.GetString(jwtId);

            if (string.IsNullOrEmpty(value))
            {
                Debug.WriteLine("Token inválido");
                context.Fail($"Token inválido");
            }

            Debug.WriteLine("Token válido: " + context.SecurityToken);
            return Task.CompletedTask;
        }
        

    }
}

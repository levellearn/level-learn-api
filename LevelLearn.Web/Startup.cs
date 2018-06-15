using AutoMapper;
using LevelLearn.Persistence.Context;
using LevelLearn.Repository.Entities.Institucional;
using LevelLearn.Repository.Entities.Jogo;
using LevelLearn.Repository.Entities.Pessoas;
using LevelLearn.Repository.Interfaces.Institucional;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Entities.Institucional;
using LevelLearn.Service.Entities.Jogo;
using LevelLearn.Service.Entities.Pessoas;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Interfaces.Jogo;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.Web.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;

namespace LevelLearn.Web
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
            services.AddAutoMapper();
            services.AddMvc()
                    .AddJsonOptions(options =>
                    {
                        options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    });

            ContextConfig(services);
            IdentityConfig(services);
            IoCInstitucional(services);
            IoCJogo(services);
            IoCPessoas(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Start}/{action=Index}/{id?}");
            });
        }

        private void ContextConfig(IServiceCollection services)
        {
            string connectionString = Configuration.GetConnectionString("LevelLearnContext");
            services.AddDbContext<LevelLearnContext>(optionBuilder =>
            {
                optionBuilder.UseSqlServer(connectionString);
            });

            services.AddDbContext<LevelLearnIdentityContext>(optBuilder =>
            {
                optBuilder.UseSqlServer(connectionString);
            });

            services.AddTransient<DbContext, LevelLearnContext>();
        }

        private void IdentityConfig(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                  .AddEntityFrameworkStores<LevelLearnIdentityContext>()
                  .AddDefaultTokenProviders();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/");
                options.LogoutPath = new PathString("/Usuarios/Logout");
                options.AccessDeniedPath = new PathString("/");
                options.Cookie = new CookieBuilder
                {
                    Name = "LevelLearnAuth",
                    Expiration = TimeSpan.FromDays(30)
                };
            });

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(30);
                options.Lockout.MaxFailedAccessAttempts = 10;

                // User settings
                options.User.RequireUniqueEmail = true;
            });
        }

        private void IoCInstitucional(IServiceCollection services)
        {
            #region Service
            services.AddTransient<ICursoService, CursoService>();
            services.AddTransient<IInstituicaoService, InstituicaoService>();
            services.AddTransient<ITurmaService, TurmaService>();
            #endregion

            #region Repository
            services.AddTransient<ICursoRepository, CursoRepository>();
            services.AddTransient<IInstituicaoRepository, InstituicaoRepository>();
            services.AddTransient<ITurmaRepository, TurmaRepository>();
            #endregion
        }

        private void IoCJogo(IServiceCollection services)
        {
            #region Service
            services.AddTransient<IChamadaService, ChamadaService>();
            services.AddTransient<IDesafioService, DesafioService>();
            services.AddTransient<IMissaoService, MissaoService>();
            services.AddTransient<IMoedaService, MoedaService>();
            services.AddTransient<IPresencaService, PresencaService>();
            services.AddTransient<IRespostaService, RespostaService>();
            services.AddTransient<ITimeService, TimeService>();
            #endregion

            #region Repository
            services.AddTransient<IChamadaRepository, ChamadaRepository>();
            services.AddTransient<IDesafioRepository, DesafioRepository>();
            services.AddTransient<IMissaoRepository, MissaoRepository>();
            services.AddTransient<IMoedaRepository, MoedaRepository>();
            services.AddTransient<IPresencaRepository, PresencaRepository>();
            services.AddTransient<IRespostaRepository, RespostaRepository>();
            services.AddTransient<ITimeRepository, TimeRepository>();
            #endregion
        }

        private void IoCPessoas(IServiceCollection services)
        {
            #region Service
            services.AddTransient<IAlunoDesafioService, AlunoDesafioService>();
            services.AddTransient<IAlunoTimeService, AlunoTimeService>();
            services.AddTransient<IAlunoTurmaService, AlunoTurmaService>();
            services.AddTransient<INotificacaoService, NotificacaoService>();
            services.AddTransient<IPessoaService, PessoaService>();
            services.AddTransient<IPessoaCursoService, PessoaCursoService>();
            services.AddTransient<IPessoaInstituicaoService, PessoaInstituicaoService>();
            #endregion

            #region Repository
            services.AddTransient<IAlunoDesafioRepository, AlunoDesafioRepository>();
            services.AddTransient<IAlunoTimeRepository, AlunoTimeRepository>();
            services.AddTransient<IAlunoTurmaRepository, AlunoTurmaRepository>();
            services.AddTransient<INotificacaoRepository, NotificacaoRepository>();
            services.AddTransient<IPessoaRepository, PessoaRepository>();
            services.AddTransient<IPessoaCursoRepository, PessoaCursoRepository>();
            services.AddTransient<IPessoaInstituicaoRepository, PessoaInstituicaoRepository>();
            #endregion
        }
    }
}

//using LevelLearn.Domain.Entities.Usuarios;
//using LevelLearn.Infra.EFCore.Configurations.Pessoas;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using System.IO;

//namespace LevelLearn.Infra.EFCore.Contexts
//{
//    public class AuthDbContext : IdentityDbContext<ApplicationUser>
//    {
//        public AuthDbContext() : base() { }

//        public AuthDbContext(DbContextOptions<AuthDbContext> options)
//            : base(options) 
//        { }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            builder.Ignore<FluentValidation.Results.ValidationFailure>();
//            builder.Ignore<FluentValidation.Results.ValidationResult>();
//            //builder.ApplyConfiguration(new PessoaConfiguration());
//            //builder.ApplyConfiguration(new ProfessorConfigurationtion());
//            //builder.ApplyConfiguration(new AlunoConfiguration());

//            builder.Entity<ApplicationUser>().HasOne(p => p.Pessoa).WithOne();
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            var config = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory())
//                .AddJsonFile("appsettings.json")
//                .Build();

//            optionsBuilder
//                .UseSqlServer(config.GetConnectionString("SQLServerConnection"));
//        }

//    }
//}

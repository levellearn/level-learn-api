using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Infra.EFCore.Configurations.Institucional;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;

namespace LevelLearn.Infra.EFCore.Contexts
{
    public class LevelLearnContext : DbContext
    {
        //public LevelLearnContext() => Database.EnsureCreated();

        public LevelLearnContext(DbContextOptions<LevelLearnContext> options)
           : base(options)
        { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        //public DbSet<Curso> Cursos { get; set; }
        //public DbSet<Turma> Turmas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.UseIdentityColumns();

            modelBuilder.Ignore<FluentValidation.Results.ValidationFailure>();
            modelBuilder.Ignore<FluentValidation.Results.ValidationResult>();

            modelBuilder.ApplyConfiguration(new InstituicaoConfiguration());
            //modelBuilder.ApplyConfiguration(new CursoConfiguration());
            //modelBuilder.ApplyConfiguration(new TurmaConfiguration());

            //modelBuilder.ApplyConfiguration(new ChamadaConfiguration());
            //modelBuilder.ApplyConfiguration(new DesafioConfiguration());
            //modelBuilder.ApplyConfiguration(new MissaoConfiguration());
            //modelBuilder.ApplyConfiguration(new MoedaConfiguration());
            //modelBuilder.ApplyConfiguration(new PresencaConfiguration());
            //modelBuilder.ApplyConfiguration(new RespostaConfiguration());
            //modelBuilder.ApplyConfiguration(new TimeConfiguration());

            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            //modelBuilder.ApplyConfiguration(new AlunoDesafioConfiguration());
            //modelBuilder.ApplyConfiguration(new AlunoTimeConfiguration());
            //modelBuilder.ApplyConfiguration(new AlunoTurmaConfiguration());
            //modelBuilder.ApplyConfiguration(new NotificacaoConfiguration());
            //modelBuilder.ApplyConfiguration(new PessoaCursoConfiguration());
            //modelBuilder.ApplyConfiguration(new PessoaInstituicaoConfiguration());

            //Remove delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            optionsBuilder
                .UseSqlServer(config.GetConnectionString("SQLServerConnection"));
        }


    }
}

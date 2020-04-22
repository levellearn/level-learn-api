using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Infra.EFCore.Configurations.Institucional;
using LevelLearn.Infra.EFCore.Configurations.Pessoas;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace LevelLearn.Infra.EFCore.Contexts
{
    public class LevelLearnContext : IdentityDbContext<Usuario>
    {
        public LevelLearnContext() : base()
        {
            Database.EnsureCreated();
        }

        public LevelLearnContext(DbContextOptions<LevelLearnContext> options)
           : base(options)
        { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Professor> Professores { get; set; }
        public DbSet<Aluno> Alunos { get; set; }
        public DbSet<Instituicao> Instituicoes { get; set; }
        public DbSet<PessoaInstituicao> PessoasInstituicoes { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<PessoaCurso> PessoasCursos { get; set; }
        public DbSet<Turma> Turmas { get; set; }
        public DbSet<AlunoTurma> AlunosTurmas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<FluentValidation.Results.ValidationFailure>();
            modelBuilder.Ignore<FluentValidation.Results.ValidationResult>();

            modelBuilder.ApplyConfiguration(new InstituicaoConfiguration());
            modelBuilder.ApplyConfiguration(new CursoConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaConfiguration());           

            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new ProfessorConfigurationtion());
            modelBuilder.ApplyConfiguration(new AlunoConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaInstituicaoConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaCursoConfiguration());
            modelBuilder.ApplyConfiguration(new AlunoTurmaConfiguration());
            //modelBuilder.ApplyConfiguration(new AlunoTimeConfiguration());
            //modelBuilder.ApplyConfiguration(new AlunoDesafioConfiguration());
            //modelBuilder.ApplyConfiguration(new NotificacaoConfiguration());

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());

            //modelBuilder.ApplyConfiguration(new ChamadaConfiguration());
            //modelBuilder.ApplyConfiguration(new DesafioConfiguration());
            //modelBuilder.ApplyConfiguration(new MissaoConfiguration());
            //modelBuilder.ApplyConfiguration(new MoedaConfiguration());
            //modelBuilder.ApplyConfiguration(new PresencaConfiguration());
            //modelBuilder.ApplyConfiguration(new RespostaConfiguration());
            //modelBuilder.ApplyConfiguration(new TimeConfiguration());

            // Remove delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                if (!relationship.IsOwnership) // !VO
                    relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }


    }
}

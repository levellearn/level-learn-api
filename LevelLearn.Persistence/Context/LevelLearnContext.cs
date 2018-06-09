using LevelLearn.Persistence.Configuration.Institucional;
using LevelLearn.Persistence.Configuration.Jogo;
using LevelLearn.Persistence.Configuration.Pessoas;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LevelLearn.Persistence.Context
{
    public class LevelLearnContext : DbContext
    {
        public LevelLearnContext(DbContextOptions<LevelLearnContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CursoConfiguration());
            modelBuilder.ApplyConfiguration(new InstituicaoConfiguration());
            modelBuilder.ApplyConfiguration(new TurmaConfiguration());

            modelBuilder.ApplyConfiguration(new ChamadaConfiguration());
            modelBuilder.ApplyConfiguration(new DesafioConfiguration());
            modelBuilder.ApplyConfiguration(new MissaoConfiguration());
            modelBuilder.ApplyConfiguration(new MoedaConfiguration());
            modelBuilder.ApplyConfiguration(new PresencaConfiguration());
            modelBuilder.ApplyConfiguration(new RespostaConfiguration());
            modelBuilder.ApplyConfiguration(new TimeConfiguration());

            modelBuilder.ApplyConfiguration(new AlunoDesafioConfiguration());
            modelBuilder.ApplyConfiguration(new AlunoTimeConfiguration());
            modelBuilder.ApplyConfiguration(new AlunoTurmaConfiguration());
            modelBuilder.ApplyConfiguration(new NotificacaoConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaCursoConfiguration());
            modelBuilder.ApplyConfiguration(new PessoaInstituicaoConfiguration());

            //Remove delete cascade
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}

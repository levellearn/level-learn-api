using LevelLearn.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Pessoas
{
    public class AlunoTurmaConfiguration : IEntityTypeConfiguration<AlunoTurma>
    {
        public void Configure(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.ToTable("AlunoTurmas");

            builder.HasKey(p => p.AlunoTurmaId);

            builder.HasOne(p => p.Aluno)
                .WithMany();

            builder.HasOne(p => p.Turma)
                .WithMany(p=> p.Alunos);
        }
    }
}

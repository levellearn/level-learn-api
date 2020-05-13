using LevelLearn.Domain.Entities.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class AlunoTurmaConfiguration : IEntityTypeConfiguration<AlunoTurma>
    {
        public void Configure(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.ToTable("AlunoTurmas");

            //builder.HasKey(p => p.Id);
            builder.HasKey(p => new { p.AlunoId, p.TurmaId }); // Chave composta

            // Relacionamentos
            builder.HasOne(p => p.Aluno)
                .WithMany();

            builder.HasOne(p => p.Turma)
                .WithMany(p => p.Alunos);
        }
    }
}

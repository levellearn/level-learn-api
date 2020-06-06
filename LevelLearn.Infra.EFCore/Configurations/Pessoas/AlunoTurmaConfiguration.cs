using LevelLearn.Domain.Entities.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class AlunoTurmaConfiguration : IEntityTypeConfiguration<AlunoTurma>
    {
        public void Configure(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.ToTable("AlunoTurmas");

            builder.HasKey(p => new { p.AlunoId, p.TurmaId }); // Chave composta

            builder.Property(p => p.DataCadastro)
                .HasDefaultValue(DateTime.UtcNow);

            // Relacionamentos
            builder.HasOne(p => p.Aluno)
                .WithMany();

            builder.HasOne(p => p.Turma)
                .WithMany(p => p.Alunos);

        }
    }
}

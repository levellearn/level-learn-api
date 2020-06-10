using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class AlunoTurmaConfiguration : ModeloAssociativoConfiguration<AlunoTurma>
    {
        public override void ConfigurarNomeTabela(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.ToTable("AlunoTurmas");
        }

        public override void ConfigurarChavePrimaria(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.HasKey(p => new { p.AlunoId, p.TurmaId }); // Chave composta
        }

        public override void ConfigurarCampos(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.Property(p => p.DataCadastro)
                .HasDefaultValue(DateTime.UtcNow);
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<AlunoTurma> builder)
        {
            builder.HasOne(p => p.Aluno)
                .WithMany();

            builder.HasOne(p => p.Turma)
                .WithMany(p => p.Alunos);
        }


    }
}

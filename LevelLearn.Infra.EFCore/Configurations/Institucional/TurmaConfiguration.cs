using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class TurmaConfiguration : EntityBaseConfiguration<Turma, Guid>
    {

        public override void ConfigurarNomeTabela(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");
        }
        public override void ConfigurarCampos(EntityTypeBuilder<Turma> builder)
        {
            builder.Property(p => p.Nome)
              .IsRequired()
              .HasMaxLength(RegraAtributo.Turma.NOME_TAMANHO_MAX)
              .HasColumnType($"varchar({RegraAtributo.Turma.NOME_TAMANHO_MAX})");

            builder.Property(p => p.NomeDisciplina)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Turma.DESCRICAO_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Turma.DESCRICAO_TAMANHO_MAX})");

            builder.Property(p => p.Meta)
               .IsRequired();
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<Turma> builder)
        {
            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Turmas);

            builder.HasMany(p => p.Alunos);
        }

       
    }
}

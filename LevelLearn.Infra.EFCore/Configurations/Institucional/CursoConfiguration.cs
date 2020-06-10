using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class CursoConfiguration : EntityBaseConfiguration<Curso, Guid>
    {
        public override void ConfigurarNomeTabela(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");
        }

        public override void ConfigurarCampos(EntityTypeBuilder<Curso> builder)
        {
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Curso.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Curso.NOME_TAMANHO_MAX})");

            builder.Property(p => p.Sigla)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Curso.SIGLA_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Curso.SIGLA_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Curso.DESCRICAO_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Curso.DESCRICAO_TAMANHO_MAX})");
        }


        public override void ConfigurarRelacionamentos(EntityTypeBuilder<Curso> builder)
        {
            builder.HasOne(p => p.Instituicao)
                .WithMany(p => p.Cursos);

            builder.HasMany(p => p.Turmas);

            builder.HasMany(p => p.Pessoas);
        }

    }
}

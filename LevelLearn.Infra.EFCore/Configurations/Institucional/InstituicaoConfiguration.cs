using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class InstituicaoConfiguration : EntityBaseConfiguration<Instituicao, Guid>
    {
        public override void ConfigurarNomeTabela(EntityTypeBuilder<Instituicao> builder)
        {
            builder.ToTable("Instituicoes");
        }

        public override void ConfigurarCampos(EntityTypeBuilder<Instituicao> builder)
        {
            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(RegraInsituicao.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraInsituicao.NOME_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
               .IsRequired(false)
               .HasMaxLength(RegraInsituicao.DESCRICAO_TAMANHO_MAX)
               .HasColumnType($"varchar({RegraInsituicao.DESCRICAO_TAMANHO_MAX})");

            //builder.HasQueryFilter(p => p.Ativo);            
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<Instituicao> builder)
        {
            builder.HasMany(p => p.Pessoas);
            builder.HasMany(p => p.Cursos);
        }


    }
}

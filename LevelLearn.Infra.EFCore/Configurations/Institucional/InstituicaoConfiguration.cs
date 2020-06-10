using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
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
                .HasMaxLength(RegraAtributo.Instituicao.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Instituicao.NOME_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
               .IsRequired()
               .HasMaxLength(RegraAtributo.Instituicao.DESCRICAO_TAMANHO_MAX)
               .HasColumnType($"varchar({RegraAtributo.Instituicao.DESCRICAO_TAMANHO_MAX})");

            //builder.HasQueryFilter(p => p.Ativo);

            //builder.HasData(
            //    new Instituicao("Instituição Teste", "Descrição Teste")
            //);
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<Instituicao> builder)
        {
            builder.HasMany(p => p.Pessoas);
            builder.HasMany(p => p.Cursos);
        }


    }
}

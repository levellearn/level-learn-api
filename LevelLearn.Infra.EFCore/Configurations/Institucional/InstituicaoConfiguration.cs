﻿using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class InstituicaoConfiguration : EntityBaseConfiguration<Instituicao>
    {
        public override void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            base.Configure(builder);
            builder.ToTable("Instituicoes");

            builder.HasKey(p => p.Id);

            builder.HasIndex(c => c.NomePesquisa).IsUnique(false);
            builder.Property(c => c.NomePesquisa).HasColumnType("varchar(250)").IsRequired();

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Instituicao.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Instituicao.NOME_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
               .IsRequired()
               .HasMaxLength(RegraAtributo.Instituicao.DESCRICAO_TAMANHO_MAX)
               .HasColumnType($"varchar({RegraAtributo.Instituicao.DESCRICAO_TAMANHO_MAX})");


            // Relacionamentos
            builder.HasMany(p => p.Pessoas);
            builder.HasMany(p => p.Cursos);

            //builder.HasQueryFilter(p => p.Ativo);


            //builder.HasData(
            //    new Instituicao("Instituição Teste", "Descrição Teste")
            //);

        }
    }
}

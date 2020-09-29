using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Validators.RegrasAtributos;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class PessoaConfiguration : EntityBaseConfiguration<Pessoa, Guid>
    {

        public override void ConfigurarNomeTabela(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");
        }

        public override void ConfigurarCampos(EntityTypeBuilder<Pessoa> builder)
        {
            builder.Property(p => p.Nome)
              .IsRequired()
              .HasMaxLength(RegraPessoa.NOME_TAMANHO_MAX)
              .HasColumnType($"varchar({RegraPessoa.NOME_TAMANHO_MAX})");

            builder.OwnsOne(c => c.Cpf)
                .Property(e => e.Numero)
                .HasColumnName("CPF")
                .HasColumnType($"varchar({RegraPessoa.CPF_TAMANHO})")
                .IsRequired(false);            

            builder.OwnsOne(c => c.Celular)
                .Property(c => c.Numero)
                .HasColumnName("Celular")
                .HasColumnType($"varchar({RegraPessoa.CELULAR_TAMANHO})")
                .IsRequired(false);

            builder.Property(p => p.DataNascimento)
               .IsRequired(false);

            builder.Property(p => p.TipoPessoa)
              .IsRequired();

            builder.Property(p => p.Genero)
              .IsRequired();
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasMany(p => p.Instituicoes);
            builder.HasMany(p => p.Cursos);
            builder.HasMany(p => p.Turmas);
        }


    }
}

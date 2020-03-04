﻿using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.NomePesquisa).IsUnique(false);
            builder.Property(c => c.NomePesquisa).HasColumnType("varchar(250)").IsRequired();

            //builder.Property("Discriminator").HasMaxLength(200);

            builder.OwnsOne(c => c.Cpf)
                .Ignore(e => e.CascadeMode)
                .Property(e => e.Number)
                .HasColumnName("CPF")
                .HasColumnType($"varchar({PropertiesConfig.Pessoa.CPF_TAMANHO})")
                .IsRequired();

            builder.OwnsOne(c => c.Email)
                .Ignore(e => e.CascadeMode)
                .Property(e => e.Address)
                .HasColumnName("Email")
                .HasColumnType($"varchar({PropertiesConfig.Pessoa.EMAIL_TAMANHO_MAX})")
                .IsRequired();

            builder.OwnsOne(c => c.Celular)
                .Ignore(e => e.CascadeMode)
                .Property(c => c.CellPhoneNumber)
                .HasColumnName("Celular")
                .HasColumnType($"varchar({PropertiesConfig.Pessoa.CELULAR_TAMANHO})")
                .IsRequired();

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(PropertiesConfig.Instituicao.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({PropertiesConfig.Instituicao.NOME_TAMANHO_MAX})")
                .IsRequired();

            builder.Property(p => p.UserName)
               .IsRequired()
               .HasMaxLength(PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX)
               .HasColumnType($"varchar({PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX})");

            builder.Property(p => p.UserName)
               .IsRequired()
               .HasMaxLength(PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX)
               .HasColumnType($"varchar({PropertiesConfig.Pessoa.USERNAME_TAMANHO_MAX})");

            builder.Property(p => p.DataNascimento)
               .IsRequired(false);

            builder.Property(p => p.ImagemUrl)
              .IsRequired();

            builder.Property(p => p.TipoPessoa)
              .IsRequired();

            builder.Property(p => p.Genero)
              .IsRequired();


            builder.HasMany(p => p.Instituicoes);
        }
    }
}

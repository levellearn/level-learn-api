using LevelLearn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration
{
    public abstract class EntityBaseConfiguration<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
        where TEntity : EntityBase<TKey>
        where TKey : IEquatable<TKey>
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigurarCamposBase(builder);
            ConfigurarNomeTabela(builder);
            ConfigurarChavePrimaria(builder);
            ConfigurarCampos(builder);
            ConfigurarRelacionamentos(builder);
        }

        public abstract void ConfigurarNomeTabela(EntityTypeBuilder<TEntity> builder);

        public virtual void ConfigurarChavePrimaria(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(c => c.Id);
        }

        public abstract void ConfigurarCampos(EntityTypeBuilder<TEntity> builder);

        public abstract void ConfigurarRelacionamentos(EntityTypeBuilder<TEntity> builder);

        public void ConfigurarCamposBase(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasIndex(c => c.NomePesquisa)
                .IsUnique(false);

            builder.Property(c => c.NomePesquisa)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .HasDefaultValue(DateTime.UtcNow);

            builder.Property(p => p.Ativo)
                .IsRequired();
        }


    }
}

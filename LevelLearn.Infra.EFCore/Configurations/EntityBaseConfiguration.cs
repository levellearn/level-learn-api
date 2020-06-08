using LevelLearn.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations
{
    public abstract class EntityBaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(c => c.Id);

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

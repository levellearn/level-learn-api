using LevelLearn.Domain.Entities.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class PessoaCursoConfiguration : IEntityTypeConfiguration<PessoaCurso>
    {
        public void Configure(EntityTypeBuilder<PessoaCurso> builder)
        {
            builder.ToTable("PessoaCursos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Perfil)
                .IsRequired();


            builder.Property(p => p.DataCadastro)
                .IsRequired(false)
                .HasDefaultValue(DateTime.UtcNow);

            // Relacionamentos
            builder.HasOne(p => p.Pessoa)
                .WithMany(p => p.Cursos);

            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Pessoas);
        }

    }
}

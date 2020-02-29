using LevelLearn.Domain.Entities.Institucional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Configurations.Institucional
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.HasOne(p => p.Instituicao)
                .WithMany(p => p.Cursos);

            builder.HasMany(p => p.Pessoas);
        }
    }
}

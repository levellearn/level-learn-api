using LevelLearn.Domain.Institucional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Institucional
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");

            builder.HasKey(p => p.CursoId);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.HasOne(p => p.Instituicao)
                .WithMany();
        }
    }
}

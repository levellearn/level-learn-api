using LevelLearn.Domain.Entities.Institucional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Configurations.Institucional
{
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(p => p.Id);

            builder.HasOne(p => p.Curso)
                .WithMany();

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.Property(p => p.Meta)
                .IsRequired();

            builder.HasMany(p => p.Alunos);
        }
    }
}

using LevelLearn.Domain.Institucional;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Institucional
{
    public class InstituicaoConfiguration : IEntityTypeConfiguration<Instituicao>
    {
        public void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            builder.ToTable("Instituicoes");

            builder.HasKey(p => p.InstituicaoId);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.HasMany(p => p.Pessoas);
            builder.HasMany(p => p.Cursos);
        }
    }
}

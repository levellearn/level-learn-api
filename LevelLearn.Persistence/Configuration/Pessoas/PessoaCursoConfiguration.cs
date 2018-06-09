using LevelLearn.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Pessoas
{
    public class PessoaCursoConfiguration : IEntityTypeConfiguration<PessoaCurso>
    {
        public void Configure(EntityTypeBuilder<PessoaCurso> builder)
        {
            builder.ToTable("PessoaCursos");

            builder.HasKey(p => p.PessoaCursoId);

            builder.Property(p => p.Perfil)
                .IsRequired();

            builder.HasOne(p => p.Pessoa)
                .WithMany();

            builder.HasOne(p => p.Curso)
                .WithMany();
        }
    }
}

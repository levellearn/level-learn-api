using LevelLearn.Domain.Jogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Jogo
{
    public class PresencaConfiguration : IEntityTypeConfiguration<Presenca>
    {
        public void Configure(EntityTypeBuilder<Presenca> builder)
        {
            builder.ToTable("Presencas");

            builder.HasKey(p => p.PresencaId);

            builder.Property(p => p.TipoPresenca)
                .IsRequired();

            builder.Property(p => p.MoedasGanha)
                .IsRequired();

            builder.HasOne(p => p.Chamada)
                .WithMany();

            builder.HasOne(p => p.Aluno)
                .WithMany();
        }
    }
}

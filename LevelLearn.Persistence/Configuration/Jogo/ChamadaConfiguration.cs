using LevelLearn.Domain.Jogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Jogo
{
    public class ChamadaConfiguration : IEntityTypeConfiguration<Chamada>
    {
        public void Configure(EntityTypeBuilder<Chamada> builder)
        {
            builder.ToTable("Chamadas");

            builder.HasKey(p => p.ChamadaId);

            builder.HasOne(p => p.Turma)
                .WithMany();

            builder.Property(p => p.DataAula)
                .IsRequired();

            builder.Property(p => p.Periodo)
                .IsRequired();
        }
    }
}

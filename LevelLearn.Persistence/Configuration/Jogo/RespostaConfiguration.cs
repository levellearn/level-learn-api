using LevelLearn.Domain.Jogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Jogo
{
    public class RespostaConfiguration : IEntityTypeConfiguration<Resposta>
    {
        public void Configure(EntityTypeBuilder<Resposta> builder)
        {
            builder.ToTable("Respostas");

            builder.HasKey(p => p.RespostaId);

            builder.Property(p => p.RespostaMissao)
                    .IsRequired();

            builder.Property(p => p.Status)
                    .IsRequired();

            builder.Property(p => p.Nota)
                    .IsRequired();

            builder.Property(p => p.MoedasGanhas)
                    .IsRequired();

            builder.HasOne(p => p.Time)
                .WithOne(p => p.Resposta)
                .HasForeignKey<Resposta>(p => p.TimeId);

            builder.HasOne(p => p.Missao)
                .WithMany();
        }
    }
}

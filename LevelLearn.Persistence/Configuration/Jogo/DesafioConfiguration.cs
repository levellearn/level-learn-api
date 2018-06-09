using LevelLearn.Domain.Jogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Jogo
{
    public class DesafioConfiguration : IEntityTypeConfiguration<Desafio>
    {
        public void Configure(EntityTypeBuilder<Desafio> builder)
        {
            builder.ToTable("Desafios");

            builder.HasKey(p => p.DesafioId);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.Property(p => p.Moedas)
                .IsRequired();

            builder.Property(p => p.Pedra)
                .IsRequired();

            builder.Property(p => p.Imagem)
                .IsRequired();

            builder.Property(p => p.IsCompletaUmaVez)
                .IsRequired();
        }
    }
}

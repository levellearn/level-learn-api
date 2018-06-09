using LevelLearn.Domain.Jogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Jogo
{
    public class MissaoConfiguration : IEntityTypeConfiguration<Missao>
    {
        public void Configure(EntityTypeBuilder<Missao> builder)
        {
            builder.ToTable("Missoes");

            builder.HasKey(p => p.MissaoId);

            builder.HasOne(p => p.Turma)
                .WithMany();

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.Property(p => p.Descricao)
                .IsRequired();

            builder.Property(p => p.Objetivo)
                .IsRequired();

            builder.Property(p => p.DataInicio)
                .IsRequired();

            builder.Property(p => p.DataTermino)
                .IsRequired();

            builder.Property(p => p.Moedas)
                .IsRequired();

            builder.Property(p => p.IsOnline)
                .IsRequired();

            builder.Property(p => p.QuantidadeMaxAlunos)
                .IsRequired();
        }
    }
}

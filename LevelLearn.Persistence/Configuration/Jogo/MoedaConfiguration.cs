using LevelLearn.Domain.Jogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Jogo
{
    public class MoedaConfiguration : IEntityTypeConfiguration<Moeda>
    {
        public void Configure(EntityTypeBuilder<Moeda> builder)
        {
            builder.ToTable("Moedas");

            builder.HasKey(p => p.MoedaId);

            builder.Property(p => p.Motivo)
                .IsRequired();

            builder.Property(p => p.MoedasGanha)
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .IsRequired();

            builder.HasOne(p => p.Aluno)
                .WithMany();

            builder.HasOne(p => p.Turma)
                .WithMany();

            builder.HasOne(p => p.Resposta)
                .WithMany();

            builder.HasOne(p => p.Presenca)
                .WithMany();

            builder.HasOne(p => p.AlunoDesafio)
                .WithMany();
        }
    }
}

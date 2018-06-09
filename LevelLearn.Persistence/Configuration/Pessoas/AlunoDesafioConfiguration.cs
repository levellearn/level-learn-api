using LevelLearn.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Pessoas
{
    public class AlunoDesafioConfiguration : IEntityTypeConfiguration<AlunoDesafio>
    {
        public void Configure(EntityTypeBuilder<AlunoDesafio> builder)
        {
            builder.ToTable("AlunoDesafios");

            builder.HasKey(p => p.AlunoDesafioId);

            builder.Property(p => p.DataCadastro)
                .IsRequired();

            builder.Property(p => p.MoedasGanha)
                .IsRequired();

            builder.HasOne(p => p.Aluno)
                .WithMany();

            builder.HasOne(p => p.Desafio)
                .WithMany();

            builder.HasOne(p => p.Turma)
                .WithMany();
        }
    }
}

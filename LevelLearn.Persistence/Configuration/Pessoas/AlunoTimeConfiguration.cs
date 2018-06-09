using LevelLearn.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Pessoas
{
    public class AlunoTimeConfiguration : IEntityTypeConfiguration<AlunoTime>
    {
        public void Configure(EntityTypeBuilder<AlunoTime> builder)
        {
            builder.ToTable("AlunoTimes");

            builder.HasKey(p => p.AlunoTimeId);

            builder.Property(p => p.IsCriador)
                .IsRequired();

            builder.HasOne(p => p.Aluno)
                .WithMany();

            builder.HasOne(p => p.Time)
                .WithMany();
        }
    }
}

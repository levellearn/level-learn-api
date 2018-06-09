using LevelLearn.Domain.Jogo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Jogo
{
    public class TimeConfiguration : IEntityTypeConfiguration<Time>
    {
        public void Configure(EntityTypeBuilder<Time> builder)
        {
            builder.ToTable("Times");

            builder.HasKey(p => p.TimeId);

            builder.Property(p => p.Nome)
                .IsRequired();
        }
    }
}

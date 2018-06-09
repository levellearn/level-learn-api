using LevelLearn.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Pessoas
{
    public class NotificacaoConfiguration : IEntityTypeConfiguration<Notificacao>
    {
        public void Configure(EntityTypeBuilder<Notificacao> builder)
        {
            builder.ToTable("Notificacoes");

            builder.HasKey(p => p.NotificacaoId);

            builder.Property(p => p.Descricao)
                .IsRequired();

            builder.Property(p => p.Titulo)
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .IsRequired();

            builder.Property(p => p.IsVisualizada)
                .IsRequired();

            builder.HasOne(p => p.Pessoa)
                .WithMany();
        }
    }
}

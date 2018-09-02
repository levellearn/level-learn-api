using LevelLearn.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Pessoas
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");

            builder.HasKey(p => p.PessoaId);

            builder.Property(p => p.Nome)
                .IsRequired();

            builder.Property(p => p.UserName)
                .IsRequired();

            builder.Property(p => p.Email)
                .IsRequired();

            builder.Property(p => p.Sexo)
                .IsRequired();

            builder.Property(p => p.DataCadastro)
                .IsRequired();

            builder.Property(p => p.Imagem)
                .IsRequired();

            builder.HasMany(p => p.Instituicoes);
        }
    }
}

using LevelLearn.Domain.Entities.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class PessoaInstituicaoConfiguration : IEntityTypeConfiguration<PessoaInstituicao>
    {
        public void Configure(EntityTypeBuilder<PessoaInstituicao> builder)
        {
            builder.ToTable("PessoaInstituicoes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Perfil)
                .IsRequired();

            // Relacionamentos
            builder.HasOne(p => p.Pessoa)
                .WithMany(p => p.Instituicoes);

            builder.HasOne(p => p.Instituicao)
                .WithMany(p => p.Pessoas);
        }

    }
}

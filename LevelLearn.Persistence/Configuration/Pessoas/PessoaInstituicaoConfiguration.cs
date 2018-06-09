using LevelLearn.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Persistence.Configuration.Pessoas
{
    public class PessoaInstituicaoConfiguration : IEntityTypeConfiguration<PessoaInstituicao>
    {
        public void Configure(EntityTypeBuilder<PessoaInstituicao> builder)
        {
            builder.ToTable("PessoaInstituicoes");

            builder.HasKey(p => p.PessoaInstituicaoId);

            builder.Property(p => p.Perfil)
                .IsRequired();

            builder.HasOne(p => p.Pessoa)
                .WithMany();

            builder.HasOne(p => p.Instituicao)
                .WithMany();
        }
    }
}

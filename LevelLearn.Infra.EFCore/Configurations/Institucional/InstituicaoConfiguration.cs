using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class InstituicaoConfiguration : IEntityTypeConfiguration<Instituicao>
    {
        public void Configure(EntityTypeBuilder<Instituicao> builder)
        {
            builder.ToTable("Instituicoes");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(PropertiesConfig.Instituicao.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({PropertiesConfig.Instituicao.NOME_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
               .IsRequired()
               .HasMaxLength(PropertiesConfig.Instituicao.DESCRICAO_TAMANHO_MAX)
               .HasColumnType($"varchar({PropertiesConfig.Instituicao.DESCRICAO_TAMANHO_MAX})");


            builder.HasMany(p => p.Pessoas);
            builder.HasMany(p => p.Cursos);
        }
    }
}

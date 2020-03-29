using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class CursoConfiguration : IEntityTypeConfiguration<Curso>
    {
        public void Configure(EntityTypeBuilder<Curso> builder)
        {
            builder.ToTable("Cursos");

            builder.HasKey(p => p.Id);

            builder.HasIndex(c => c.NomePesquisa).IsUnique(false);
            builder.Property(c => c.NomePesquisa).HasColumnType("varchar(250)").IsRequired();

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(PropertiesConfig.Curso.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({PropertiesConfig.Curso.NOME_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(PropertiesConfig.Curso.DESCRICAO_TAMANHO_MAX)
                .HasColumnType($"varchar({PropertiesConfig.Curso.DESCRICAO_TAMANHO_MAX})");

            // Relacionamentos

            builder.HasOne(p => p.Instituicao)
                .WithMany(p => p.Cursos);

            builder.HasMany(p => p.Turmas);

            builder.HasMany(p => p.Pessoas);
        }
    }
}

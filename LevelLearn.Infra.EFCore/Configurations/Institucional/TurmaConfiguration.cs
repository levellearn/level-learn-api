using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class TurmaConfiguration : IEntityTypeConfiguration<Turma>
    {
        public void Configure(EntityTypeBuilder<Turma> builder)
        {
            builder.ToTable("Turmas");

            builder.HasKey(p => p.Id);

            builder.HasIndex(c => c.NomePesquisa).IsUnique(false);
            builder.Property(c => c.NomePesquisa).HasColumnType("varchar(250)").IsRequired();

            builder.Property(p => p.Nome)
              .IsRequired()
              .HasMaxLength(PropertiesConfig.Turma.NOME_TAMANHO_MAX)
              .HasColumnType($"varchar({PropertiesConfig.Turma.NOME_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(PropertiesConfig.Turma.DESCRICAO_TAMANHO_MAX)
                .HasColumnType($"varchar({PropertiesConfig.Turma.DESCRICAO_TAMANHO_MAX})");

            builder.Property(p => p.Meta)
               .IsRequired();

            // Relacionamentos
            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Turmas);         

            builder.HasMany(p => p.Alunos);
        }
    }
}

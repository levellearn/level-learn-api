using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Institucional
{
    public class TurmaConfiguration : EntityBaseConfiguration<Turma>
    {
        public override void Configure(EntityTypeBuilder<Turma> builder)
        {
            base.Configure(builder);
            builder.ToTable("Turmas");
           
            builder.Property(p => p.Nome)
              .IsRequired()
              .HasMaxLength(RegraAtributo.Turma.NOME_TAMANHO_MAX)
              .HasColumnType($"varchar({RegraAtributo.Turma.NOME_TAMANHO_MAX})");

            builder.Property(p => p.NomeDisciplina)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Turma.NOME_DISCIPLINA_TAMANHO_MAX})");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Turma.DESCRICAO_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Turma.DESCRICAO_TAMANHO_MAX})");

            builder.Property(p => p.Meta)
               .IsRequired();

            // Relacionamentos
            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Turmas);         

            builder.HasMany(p => p.Alunos);
        }
    }
}

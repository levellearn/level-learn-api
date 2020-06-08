using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class PessoaConfiguration : EntityBaseConfiguration<Pessoa>
    {
        public override void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            base.Configure(builder);

            builder.ToTable("Pessoas");

            builder.Property(p => p.Nome)
               .IsRequired()
               .HasMaxLength(RegraAtributo.Pessoa.NOME_TAMANHO_MAX)
               .HasColumnType($"varchar({RegraAtributo.Pessoa.NOME_TAMANHO_MAX})");

            builder.OwnsOne(c => c.Cpf)
                .Property(e => e.Numero)
                .HasColumnName("CPF")
                .HasColumnType($"varchar({RegraAtributo.Pessoa.CPF_TAMANHO})")
                .IsRequired(false);

            builder.OwnsOne(c => c.Email)
                .Property(e => e.Endereco)
                .HasColumnName("Email")
                .HasColumnType($"varchar({RegraAtributo.Usuario.EMAIL_TAMANHO_MAX})")
                .IsRequired(false);

            builder.OwnsOne(c => c.Celular)
                .Property(c => c.Numero)
                .HasColumnName("Celular")
                .HasColumnType($"varchar({RegraAtributo.Pessoa.CELULAR_TAMANHO})")
                .IsRequired(false);         

            builder.Property(p => p.DataNascimento)
               .IsRequired(false);           

            builder.Property(p => p.TipoPessoa)
              .IsRequired();

            builder.Property(p => p.Genero)
              .IsRequired();

            // Relacionamentos

            builder.HasMany(p => p.Instituicoes);
            builder.HasMany(p => p.Cursos);
            builder.HasMany(p => p.Turmas);
            //builder.HasMany(p => p.Turmas)
            //    .WithOne(t => t.Professor)
            //    .HasForeignKey(t => t.ProfessorId);

        }
    }
}

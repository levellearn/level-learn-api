using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class PessoaConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable("Pessoas");

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.NomePesquisa).IsUnique(false);
            builder.Property(c => c.NomePesquisa).HasColumnType("varchar(250)").IsRequired();

            //builder.Property("Discriminator").HasMaxLength(200);

            builder.OwnsOne(c => c.Cpf)
                .Ignore(e => e.CascadeMode)
                .Property(e => e.Numero)
                .HasColumnName("CPF")
                .HasColumnType($"varchar({RegraAtributo.Pessoa.CPF_TAMANHO})")
                .IsRequired(false);

            builder.OwnsOne(c => c.Email)
                .Ignore(e => e.CascadeMode)
                .Property(e => e.Endereco)
                .HasColumnName("Email")
                .HasColumnType($"varchar({RegraAtributo.Pessoa.EMAIL_TAMANHO_MAX})")
                .IsRequired(false);

            builder.OwnsOne(c => c.Celular)
                .Ignore(e => e.CascadeMode)
                .Property(c => c.Numero)
                .HasColumnName("Celular")
                .HasColumnType($"varchar({RegraAtributo.Pessoa.CELULAR_TAMANHO})")
                .IsRequired(false);

            builder.Property(p => p.Nome)
                .IsRequired()
                .HasMaxLength(RegraAtributo.Instituicao.NOME_TAMANHO_MAX)
                .HasColumnType($"varchar({RegraAtributo.Instituicao.NOME_TAMANHO_MAX})");

            builder.Property(p => p.NickName)
               .IsRequired()
               .HasMaxLength(RegraAtributo.Pessoa.NICKNAME_TAMANHO_MAX)
               .HasColumnType($"varchar({RegraAtributo.Pessoa.NICKNAME_TAMANHO_MAX})");           

            builder.Property(p => p.DataNascimento)
               .IsRequired(false);

            builder.Property(p => p.ImagemUrl)
              .IsRequired();

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

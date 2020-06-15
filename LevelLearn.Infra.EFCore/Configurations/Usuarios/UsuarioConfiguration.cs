using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.RegrasAtributos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Ignore(p => p.Senha);

            builder.Ignore(p => p.ConfirmacaoSenha);

            builder.Property(p => p.ImagemUrl)
                .IsRequired();

            builder.Property(p => p.NickName)
              .IsRequired()
              .HasMaxLength(RegraUsuario.NICKNAME_TAMANHO_MAX)
              .HasColumnType($"varchar({RegraUsuario.NICKNAME_TAMANHO_MAX})");

            builder.HasOne(p => p.Pessoa)
                .WithOne();
        }

    }
}

using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class PessoaInstituicaoConfiguration : ModeloAssociativoConfiguration<PessoaInstituicao>
    {
        public override void ConfigurarNomeTabela(EntityTypeBuilder<PessoaInstituicao> builder)
        {
            builder.ToTable("PessoaInstituicoes");
        }

        public override void ConfigurarChavePrimaria(EntityTypeBuilder<PessoaInstituicao> builder)
        {
            builder.HasKey(p => new { p.PessoaId, p.InstituicaoId }); // Chave composta
        }

        public override void ConfigurarCampos(EntityTypeBuilder<PessoaInstituicao> builder)
        {
            builder.Property(p => p.Perfil)
               .IsRequired();

            builder.Property(p => p.DataCadastro)
                 .HasDefaultValue(DateTime.UtcNow);
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<PessoaInstituicao> builder)
        {
            builder.HasOne(p => p.Pessoa)
               .WithMany(p => p.Instituicoes);

            builder.HasOne(p => p.Instituicao)
                .WithMany(p => p.Pessoas);
        }

    }
}

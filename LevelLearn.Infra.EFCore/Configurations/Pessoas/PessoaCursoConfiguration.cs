using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class PessoaCursoConfiguration : ModeloAssociativoConfiguration<PessoaCurso>
    {
        public override void ConfigurarNomeTabela(EntityTypeBuilder<PessoaCurso> builder)
        {
            builder.ToTable("PessoaCursos");
        }

        public override void ConfigurarChavePrimaria(EntityTypeBuilder<PessoaCurso> builder)
        {
            builder.HasKey(p => new { p.PessoaId, p.CursoId }); // Chave composta
        }

        public override void ConfigurarCampos(EntityTypeBuilder<PessoaCurso> builder)
        {
            builder.Property(p => p.Perfil)
                 .IsRequired();

            builder.Property(p => p.DataCadastro)
                .HasDefaultValue(DateTime.UtcNow);
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<PessoaCurso> builder)
        {
            builder.HasOne(p => p.Pessoa)
               .WithMany(p => p.Cursos);

            builder.HasOne(p => p.Curso)
                .WithMany(p => p.Pessoas);            
        }        

    }
}

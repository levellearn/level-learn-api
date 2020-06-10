using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class AlunoConfiguration : EntityBaseConfiguration<Aluno, Guid>
    {

        public override void ConfigurarNomeTabela(EntityTypeBuilder<Aluno> builder) { }

        public override void ConfigurarCampos(EntityTypeBuilder<Aluno> builder)
        {
            builder.Property(p => p.RA)
                .IsRequired(false);
        }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<Aluno> builder) { }
      

    }
}

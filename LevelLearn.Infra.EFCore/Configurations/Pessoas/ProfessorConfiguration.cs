using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class ProfessorConfigurationtion : EntityBaseConfiguration<Professor, Guid>
    {
        public override void ConfigurarNomeTabela(EntityTypeBuilder<Professor> builder) { }

        public override void ConfigurarCampos(EntityTypeBuilder<Professor> builder) { }

        public override void ConfigurarRelacionamentos(EntityTypeBuilder<Professor> builder) { }
        

    }

}

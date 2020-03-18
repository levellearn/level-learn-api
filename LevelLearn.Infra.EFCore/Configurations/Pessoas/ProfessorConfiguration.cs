using LevelLearn.Domain.Entities.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.Pessoas
{
    public class ProfessorConfigurationtion : IEntityTypeConfiguration<Professor>
    {
        public void Configure(EntityTypeBuilder<Professor> builder)
        {


        }

    }

}

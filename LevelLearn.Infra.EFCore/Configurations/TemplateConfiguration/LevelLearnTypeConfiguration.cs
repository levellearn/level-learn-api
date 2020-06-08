using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration
{
    public abstract class LevelLearnTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
        where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigurarNomeTabela(builder);
            ConfigurarIndices(builder);
            ConfigurarCampos(builder);
            ConfigurarChavePrimaria(builder);
            ConfigurarRelacionamentos(builder);
        }

        public abstract void ConfigurarNomeTabela(EntityTypeBuilder<TEntity> builder);
        public abstract void ConfigurarIndices(EntityTypeBuilder<TEntity> builder);
        public abstract void ConfigurarCampos(EntityTypeBuilder<TEntity> builder);
        public abstract void ConfigurarChavePrimaria(EntityTypeBuilder<TEntity> builder);
        public abstract void ConfigurarRelacionamentos(EntityTypeBuilder<TEntity> builder);

    }

}

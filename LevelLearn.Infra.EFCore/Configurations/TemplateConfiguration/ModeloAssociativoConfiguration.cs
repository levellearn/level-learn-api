using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LevelLearn.Infra.EFCore.Configurations.TemplateConfiguration
{
    /// <summary>
    /// Utilizado para configurar tableas N para N
    /// </summary>
    /// <typeparam name="TEntity">Tipo da entidade</typeparam>
    public abstract class ModeloAssociativoConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> 
        where TEntity : class
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigurarNomeTabela(builder);
            ConfigurarChavePrimaria(builder);
            ConfigurarCampos(builder);
            ConfigurarRelacionamentos(builder);
        }

        public abstract void ConfigurarNomeTabela(EntityTypeBuilder<TEntity> builder);
        public abstract void ConfigurarChavePrimaria(EntityTypeBuilder<TEntity> builder);
        public abstract void ConfigurarCampos(EntityTypeBuilder<TEntity> builder);
        public abstract void ConfigurarRelacionamentos(EntityTypeBuilder<TEntity> builder);

    }

}

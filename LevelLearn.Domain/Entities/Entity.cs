using System;

namespace LevelLearn.Domain.Entities
{
    /// <summary>
    /// Entidade padrão tendo o ID como um Guid
    /// </summary>
    public abstract class Entity : EntityBase<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }

}

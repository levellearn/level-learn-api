using System;

namespace LevelLearn.Domain.Entities
{
    public abstract class Entity : EntityBase<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }
    }

}

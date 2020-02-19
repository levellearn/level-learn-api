using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities
{
    public abstract class EntityBase
    {
        // Ctor
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }

        // Props
        public Guid Id { get; private set; }
        public ValidationResult ValidationResult { get; protected set; }

        // Methods
        public abstract bool Valido();

        /// <summary>
        /// Retorna os dados inválidos da entidade
        /// </summary>
        /// <returns>Retorna uma lista de dados inválidos da entidade</returns>
        public ICollection<ValidationFailure> DadosInvalidos()
        {
            return ValidationResult.Errors;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return this.Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} [Id = {Id}]";
        }

    }


}

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
        public bool Ativo { get; protected set; }
        public string NomePesquisa { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        // Methods

        /// <summary>
        /// Método que deve ser implementado para saber se entidade está válida
        /// </summary>
        /// <returns></returns>
        public abstract bool EstaValido();

        /// <summary>
        /// Retorna os dados inválidos da entidade
        /// </summary>
        /// <returns>Retorna uma lista de dados inválidos da entidade</returns>
        public ICollection<ValidationFailure> DadosInvalidos()
        {
            return ValidationResult.Errors;
        }

        /// <summary>
        /// Muda o estado para ativado da entidade
        /// </summary>
        public virtual void Ativar()
        {
            Ativo = true;
        }

        /// <summary>
        /// Muda o estado para desativado da entidade
        /// </summary>
        public virtual void Desativar()
        {
            Ativo = false;
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

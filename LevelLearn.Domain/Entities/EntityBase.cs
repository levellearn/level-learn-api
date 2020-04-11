using FluentValidation.Results;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities
{
    public abstract class EntityBase
    {
        protected EntityBase()
        {
            Id = Guid.NewGuid();
            Ativo = true;
            DataCadastro = DateTime.Now;
            ResultadoValidacao = new ValidationResult();
        }

        #region Props

        public Guid Id { get; private set; }
        public bool Ativo { get; protected set; }
        public string NomePesquisa { get; protected set; }
        public DateTime DataCadastro { get; private set; }
        public ValidationResult ResultadoValidacao { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Método que deve ser implementado para saber se a entidade está válida
        /// </summary>
        /// <returns></returns>
        public abstract bool EstaValido();

        /// <summary>
        /// Retorna os dados inválidos da entidade
        /// </summary>
        /// <returns>Retorna uma lista de dados inválidos da entidade</returns>
        public ICollection<DadoInvalido> DadosInvalidos()
        {
            return ResultadoValidacao.GetErrorsResult();
        }

        /// <summary>
        /// Muda o estado da entidade para ativado
        /// </summary>
        public virtual void Ativar()
        {
            Ativo = true;
        }

        /// <summary>
        /// Muda o estado da entidade para desativado
        /// </summary>
        public virtual void Desativar()
        {
            Ativo = false;
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return this.Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
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

        #endregion

    }


}

using FluentValidation.Results;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators;
using System;
using System.Collections.Generic;

namespace LevelLearn.Domain.Entities
{
    /// <summary>
    /// Entidade base para todas as entidades do domínio
    /// </summary>
    /// <typeparam name="TKey">O tipo usado como ID</typeparam>
    public abstract class EntityBase<TKey> where TKey : IEquatable<TKey>
    {
        protected EntityBase()
        {
            Ativo = true;
            DataCadastro = DateTime.UtcNow;
            ResultadoValidacao = new ValidationResult();
        }

        #region Props

        public TKey Id { get; protected set; }
        public bool Ativo { get; protected set; }
        public string NomePesquisa { get; protected set; }
        public DateTime DataCadastro { get; private set; }
        public ValidationResult ResultadoValidacao { get; protected set; }

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

        /// <summary>
        /// Método que deve ser implementado para atribuir o nome de pesquisa para a entidade
        /// </summary>
        public abstract void AtribuirNomePesquisa();


        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return this.Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityBase<TKey> a, EntityBase<TKey> b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<TKey> a, EntityBase<TKey> b)
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

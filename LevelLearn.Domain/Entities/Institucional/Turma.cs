using LevelLearn.Domain.Entities.Pessoas;
using System;
using System.Collections.Generic;
using System.Text;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Turma : EntityBase
    {
        #region Ctors
        protected Turma() { }

        public Turma(string nome, string descricao, Guid cursoId, Guid professorId)
        {
            Nome = nome;
            Descricao = descricao;
            Meta = decimal.Zero;
            CursoId = cursoId;
            ProfessorId = professorId;
            Alunos = new List<AlunoTurma>();
        }

        #endregion Ctors

        #region Props

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public decimal Meta { get; private set; }

        public Guid CursoId { get; private set; }
        public virtual Curso Curso { get; private set; }

        public Guid ProfessorId { get; private set; }
        public virtual Professor Professor { get; private set; }

        public virtual ICollection<AlunoTurma> Alunos { get; private set; }

        #endregion Props

        #region Methods

        public override bool EstaValido()
        {
            throw new NotImplementedException();

            // var validator = new TurmaValidator();
            // this.ValidationResult = validator.Validate(this);

            return this.ValidationResult.IsValid;
        }

        #endregion


    }
}

using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Validators.Institucional;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Domain.Entities.Institucional
{
    public class Turma : EntityBase
    {
        #region Ctors
        protected Turma()
        {
            Alunos = new List<AlunoTurma>();
        }

        public Turma(string nome, string descricao, string nomeDisciplina, Guid cursoId, Guid professorId)
        {
            Nome = nome.RemoveExtraSpaces();
            Descricao = descricao?.Trim();
            Meta = 0d;
            NomeDisciplina = nomeDisciplina.RemoveExtraSpaces();
            CursoId = cursoId;
            ProfessorId = professorId;
            Alunos = new List<AlunoTurma>();

            AtribuirNomePesquisa();
        }

        #endregion Ctors

        #region Props

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double Meta { get; private set; }
        public string NomeDisciplina { get; private set; }

        public Guid CursoId { get; private set; }
        public Curso Curso { get; private set; }

        public Guid ProfessorId { get; private set; }
        public Professor Professor { get; private set; }

        public ICollection<AlunoTurma> Alunos { get; private set; }

        #endregion Props

        #region Methods

        public void AtribuirAluno(AlunoTurma aluno)
        {
            if (!Alunos.Any(at => at.AlunoId == aluno.AlunoId))
                Alunos.Add(aluno);
        }

        public void AtribuirAlunos(ICollection<AlunoTurma> alunos)
        {
            foreach (AlunoTurma aluno in alunos)
            {
                if (!Alunos.Any(at => at.AlunoId == aluno.AlunoId))
                    Alunos.Add(aluno);
            }
        }

        public decimal CalcularMeta()
        {
            throw new NotImplementedException();
        }

        public override bool EstaValido()
        {
            var validator = new TurmaValidator();
            this.ResultadoValidacao = validator.Validate(this);

            return this.ResultadoValidacao.IsValid;
        }

        /// <summary>
        /// Gerado a partir do nome e nome da disciplina
        /// </summary>
        public override void AtribuirNomePesquisa()
        {
            NomePesquisa = string.Concat(Nome, NomeDisciplina).GenerateSlug();
        }

        public void Atualizar(string nome, string descricao, string nomeDisciplina)
        {
            Nome = nome.RemoveExtraSpaces();
            Descricao = descricao?.Trim();
            NomeDisciplina = nomeDisciplina.RemoveExtraSpaces();

            AtribuirNomePesquisa();
        }

        #endregion Methods


    }
}

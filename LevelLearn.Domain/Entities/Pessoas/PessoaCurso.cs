using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Enums;
using System;

namespace LevelLearn.Domain.Entities.Pessoas
{
    public class PessoaCurso
    {
        protected PessoaCurso() { }

        public PessoaCurso(TiposPessoa perfil, Guid pessoaId, Guid cursoId)
        {
            Id = Guid.NewGuid();
            Perfil = perfil;
            PessoaId = pessoaId;
            CursoId = cursoId;
        }

        public Guid Id { get; private set; }
        public TiposPessoa Perfil { get; private set; }

        public Guid PessoaId { get; private set; }
        public virtual Pessoa Pessoa { get; private set; }

        public Guid CursoId { get; private set; }
        public virtual Curso Curso { get; private set; }

    }
}

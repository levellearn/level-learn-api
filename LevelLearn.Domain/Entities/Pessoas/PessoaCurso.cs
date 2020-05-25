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
            DataCadastro = DateTime.UtcNow;
        }

        public Guid Id { get; set; }
        public TiposPessoa Perfil { get; set; }

        public Guid PessoaId { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public Guid CursoId { get; set; }
        public virtual Curso Curso { get; set; }
        public DateTime? DataCadastro { get; private set; }


    }
}

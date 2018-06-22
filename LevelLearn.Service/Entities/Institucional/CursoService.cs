using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Institucional;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Institucional;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Service.Entities.Institucional
{
    public class CursoService : CrudService<Curso>, ICursoService
    {
        private readonly ICursoRepository _cursoRepository;
        public CursoService(ICursoRepository cursoRepository)
            : base(cursoRepository)
        {
            _cursoRepository = cursoRepository;
        }

        public bool Insert(Curso curso, List<int> professores, List<int> alunos)
        {
            foreach (var item in professores)
            {
                curso.Pessoas.Add(new PessoaCurso
                {
                    CursoId = curso.CursoId,
                    Perfil = TipoPessoaEnum.Professor,
                    PessoaId = item
                });
            }

            foreach (var item in alunos)
            {
                curso.Pessoas.Add(new PessoaCurso
                {
                    CursoId = curso.CursoId,
                    Perfil = TipoPessoaEnum.Aluno,
                    PessoaId = item
                });
            }

            return _cursoRepository.Insert(curso);
        }

        public bool IsProfessor(int cursoId, int pessoaId)
        {
            return _cursoRepository.IsProfessor(cursoId, pessoaId);
        }

        public List<StatusResponseEnum> ValidaCurso(Curso curso)
        {
            List<StatusResponseEnum> valida = new List<StatusResponseEnum>();

            Curso validaCurso = new Curso();

            validaCurso = _cursoRepository.Select(p => p.Nome.ToUpper().Trim() == curso.Nome.ToUpper().Trim() && p.InstituicaoId == curso.InstituicaoId).FirstOrDefault();

            if ((validaCurso != null) && (validaCurso.CursoId != curso.CursoId))
                valida.Add(StatusResponseEnum.CursoExistenteInstituicao);

            return valida;
        }
    }
}

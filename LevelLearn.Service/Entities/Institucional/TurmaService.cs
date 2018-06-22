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
    public class TurmaService : CrudService<Turma>, ITurmaService
    {
        private readonly ITurmaRepository _turmaRepository;
        public TurmaService(ITurmaRepository turmaRepository)
            : base(turmaRepository)
        {
            _turmaRepository = turmaRepository;
        }

        public bool Insert(Turma turma, List<int> alunos)
        {
            foreach (var item in alunos)
            {
                turma.Alunos.Add(new AlunoTurma
                {
                    TurmaId = turma.TurmaId,
                    AlunoId = item
                });
            }

            return _turmaRepository.Insert(turma);
        }

        public bool IsTurmaDoProfessor(int turmaId, int professorId)
        {
            return _turmaRepository.Select(p => p.TurmaId == turmaId && p.ProfessorId == professorId).Count > 0;
        }

        public List<StatusResponseEnum> ValidaTurma(Turma turma)
        {
            List<StatusResponseEnum> valida = new List<StatusResponseEnum>();

            Turma validaTurma = new Turma();

            validaTurma = _turmaRepository.Select(p => p.Nome.ToUpper().Trim() == turma.Nome.ToUpper().Trim() && p.CursoId == turma.CursoId).FirstOrDefault();

            if ((validaTurma != null) && (validaTurma.TurmaId != turma.TurmaId))
                valida.Add(StatusResponseEnum.TurmaExistenteNoCurso);

            return valida;
        }
    }
}

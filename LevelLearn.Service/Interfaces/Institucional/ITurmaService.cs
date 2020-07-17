using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Turma;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface ITurmaService : IServiceBase<Turma, Guid>, IDisposable
    {
        Task<ResultadoService<IEnumerable<Turma>>> TurmasProfessorPorCurso(Guid cursoId, Guid pessoaId, FiltroPaginacao filtroPaginacao);
        Task<ResultadoService<IEnumerable<Turma>>> TurmasAluno(Guid pessoaId, FiltroPaginacao filtroPaginacao);
        Task<ResultadoService<Turma>> ObterTurma(Guid turmaId, Guid pessoaId);
        Task<ResultadoService<Turma>> CadastrarTurma(Turma turma, Guid pessoaId);
        Task<ResultadoService<Turma>> EditarTurma(Guid turmaId, EditarTurmaVM turmaVM, Guid pessoaId);
        Task<ResultadoService<Turma>> AlternarAtivacaoTurma(Guid turmaId, Guid pessoaId);
        Task<ResultadoService<Turma>> IncluirAlunosNaTurma(Guid turmaId, Guid professorId, ICollection<Guid> idsAluno);
    }
}

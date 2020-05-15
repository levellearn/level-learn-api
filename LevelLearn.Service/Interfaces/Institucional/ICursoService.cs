using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface ICursoService : IServiceBase<Curso>, IDisposable
    {
        Task<ResponseAPI<IEnumerable<Curso>>> ObterCursosProfessor(Guid instituicaoId, Guid pessoaId, PaginationFilterVM filterVM);
        Task<ResponseAPI<Curso>> ObterCurso(Guid cursoId, Guid pessoaId);
        Task<ResponseAPI<Curso>> CadastrarCurso(CadastrarCursoVM cursoVM, Guid pessoaId);
        Task<ResponseAPI<Curso>> EditarCurso(Guid cursoId, EditarCursoVM cursoVM, Guid pessoaId);
        Task<ResponseAPI<Curso>> RemoverCurso(Guid cursoId, Guid pessoaId);
    }
}

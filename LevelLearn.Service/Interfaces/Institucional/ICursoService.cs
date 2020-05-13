using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Curso;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface ICursoService : IServiceBase<Curso>, IDisposable
    {
        //Task<ResponseAPI<Curso>> ObterCurso(Guid id, string pessoaId);
        //Task<ResponseAPI<IEnumerable<Curso>>> ObterCursosProfessor(string pessoaId, PaginationQueryVM queryVM);
        Task<ResponseAPI<Curso>> CadastrarCurso(CadastrarCursoVM instituicaoVM, string pessoaId);
        //Task<ResponseAPI<Curso>> EditarCurso(Guid id, EditarCursoVM instituicaoVM, string pessoaId);
        //Task<ResponseAPI<Curso>> RemoverCurso(Guid id, string pessoaId);
    }
}

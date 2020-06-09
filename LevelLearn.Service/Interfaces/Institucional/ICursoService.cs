using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface ICursoService : IServiceBase<Curso, Guid>, IDisposable
    {
        Task<ResponseAPI<IEnumerable<Curso>>> CursosInstituicaoProfessor(Guid instituicaoId, Guid pessoaId, FiltroPaginacao filtroPaginacao);
        Task<ResponseAPI<Curso>> ObterCurso(Guid cursoId, Guid pessoaId);
        Task<ResponseAPI<Curso>> CadastrarCurso(CadastrarCursoVM cursoVM, Guid pessoaId);
        Task<ResponseAPI<Curso>> EditarCurso(Guid cursoId, EditarCursoVM cursoVM, Guid pessoaId);
        Task<ResponseAPI<Curso>> AlternarAtivacaoCurso(Guid cursoId, Guid pessoaId);
    }
}

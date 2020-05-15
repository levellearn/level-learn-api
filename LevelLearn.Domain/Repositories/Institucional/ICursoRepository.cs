using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Institucional
{
    public interface ICursoRepository : IRepositoryBase<Curso>
    {
        /// <summary>
        /// Retorna os cursos de um professor, paginadas com filtro por nome
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <returns>Lista de cursos</returns>
        Task<List<Curso>> CursosProfessor(Guid pessoaId, string searchFilter, int pageNumber, int pageSize);

        /// <summary>
        /// Retorna o total de cursos de um professor para a paginação
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <returns>Total de cursos</returns>
        Task<int> TotalCursosProfessor(Guid pessoaId, string searchFilter);

        /// <summary>
        /// Retorna os cursos de uma instituição de um professor, paginadas com filtro por nome
        /// </summary>
        /// <param name="instituicaoId">Id instituição</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <param name="pageNumber">Número da página</param>
        /// <param name="pageSize">Quantidade de itens por página</param>
        /// <returns>Lista de cursos</returns>
        Task<List<Curso>> CursosInstituicaoProfessor(Guid instituicaoId, Guid pessoaId, string searchFilter, int pageNumber, int pageSize);

        /// <summary>
        /// Retorna o total cursos de uma instituição de um professor para a paginação
        /// </summary>
        /// <param name="instituicaoId">Id instituição</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <returns>Total de cursos</returns>
        Task<int> TotalCursosInstituicaoProfessor(Guid instituicaoId, Guid pessoaId, string searchFilter);

        /// <summary>
        /// Verifica se é professor de um determinado curso
        /// </summary>
        /// <param name="cursoId">Id curso</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <returns></returns>
        Task<bool> IsProfessorDoCurso(Guid cursoId, Guid pessoaId);            

    }
}

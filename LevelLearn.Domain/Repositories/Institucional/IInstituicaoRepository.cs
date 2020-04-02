using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Institucional
{
    public interface IInstituicaoRepository : IRepositoryBase<Instituicao>
    {
        /// <summary>
        /// Verifica se é professor admin de uma determinada instituição
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="pessoaId"></param>
        /// <returns>Retorna verdadeiro ou falso</returns>
        Task<bool> IsAdmin(Guid instituicaoId, Guid pessoaId);

        /// <summary>
        /// Retorna todas as instituições de um professor admin
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <returns>Lista de instituições</returns>
        Task<List<Instituicao>> InstituicoesAdmin(Guid pessoaId);

        /// <summary>
        /// Retorna todas as instituições de um professor PAGINADAS
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <param name="query">Termo de pesquisa</param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns>Lista de instituições</returns>
        Task<List<Instituicao>> InstituicoesProfessor(Guid pessoaId, string query, int pageNumber, int pageSize);

        /// <summary>
        /// Retorna o total de instituições de um professor PARA PAGINAÇÃO
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <param name="query">Termo de pesquisa</param>
        /// <returns></returns>
        Task<int> TotalInstituicoesProfessor(Guid pessoaId, string query);

        /// <summary>
        /// Retorna todas as instituições de um aluno
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <returns>Lista de instituições</returns>
        Task<List<Instituicao>> InstituicoesAluno(Guid pessoaId);

    }
}

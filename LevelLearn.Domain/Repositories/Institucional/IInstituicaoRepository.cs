using LevelLearn.Domain.Entities.Comum;
using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Institucional
{
    public interface IInstituicaoRepository : IRepositoryBase<Instituicao, Guid>
    {
        /// <summary>
        /// Retorna a instituição com todos os dados relacionados
        /// </summary>
        /// <param name="id">Id instituição</param>
        /// <returns>Instituição</returns>
        Task<Instituicao> InstituicaoCompleta(Guid id);

        /// <summary>
        /// Verifica se é professor admin de uma determinada instituição
        /// </summary>
        /// <param name="instituicaoId">Id instituição</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <returns></returns>
        Task<bool> ProfessorAdmin(Guid instituicaoId, Guid pessoaId);

        /// <summary>
        /// Retorna todas as instituições de um professor admin
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <returns>Lista de instituições</returns>
        Task<IEnumerable<Instituicao>> InstituicoesProfessorAdmin(Guid pessoaId);

        /// <summary>
        /// Retorna as instituições de um professor paginadas com filtro
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPaginacao">Filtros da paginação</param>       
        /// <returns>Lista de instituições</returns>
        Task<IEnumerable<Instituicao>> InstituicoesProfessor(Guid pessoaId, FiltroPaginacao filtroPaginacao);

        /// <summary>
        /// Retorna o total de instituições de um professor para a paginação
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPesquisa">Termo de pesquisa</param>
        /// <param name="ativo">Entidade ativa</param>
        /// <returns>Total de instituições/returns>
        Task<int> TotalInstituicoesProfessor(Guid pessoaId, string filtroPesquisa, bool ativo = true);

        /// <summary>
        /// Retorna todas as instituições de um aluno
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <returns>Lista de instituições</returns>
        Task<IEnumerable<Instituicao>> InstituicoesAluno(Guid pessoaId);

    }
}

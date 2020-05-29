using LevelLearn.Domain.Entities.Institucional;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Institucional
{
    public interface IInstituicaoRepository : IRepositoryBase<Instituicao>
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
        Task<List<Instituicao>> InstituicoesProfessorAdmin(Guid pessoaId);

        /// <summary>
        /// Retorna as instituições de um professor paginadas com filtro
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="termoPesquisa">Termo de pesquisa</param>
        /// <param name="numeroPagina">Número da página</param>
        /// <param name="tamanhoPorPagina">Quantidade de itens por página</param>
        /// <param name="ordernarPor">Nome do campo para ordenação</param>
        /// <param name="ordenacaoAscendente">Tipo de ordenação</param>
        /// <param name="ativo">Entidade ativa</param>
        /// <returns>Lista de instituições</returns>
        Task<List<Instituicao>> InstituicoesProfessor(Guid pessoaId, string termoPesquisa, int numeroPagina, int tamanhoPorPagina, string ordernarPor, bool ordenacaoAscendente, bool ativo);

        /// <summary>
        /// Retorna o total de instituições de um professor para a paginação
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="searchFilter">Termo de pesquisa</param>
        /// <returns>Total de instituições/returns>
        Task<int> TotalInstituicoesProfessor(Guid pessoaId, string searchFilter);

        /// <summary>
        /// Retorna todas as instituições de um aluno
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <returns>Lista de instituições</returns>
        Task<List<Instituicao>> InstituicoesAluno(Guid pessoaId);

    }
}

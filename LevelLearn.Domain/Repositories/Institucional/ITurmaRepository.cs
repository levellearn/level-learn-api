using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Utils.Comum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Institucional
{
    public interface ITurmaRepository : IRepositoryBase<Turma, Guid>
    {
        /// <summary>
        /// Retorna as turmas de um curso de um professor paginadas com filtro
        /// </summary>
        /// <param name="cursoId">Id curso</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPaginacao">Filtros da paginação</param>       
        /// <returns>Lista de turmas</returns>
        Task<IEnumerable<Turma>> TurmasProfessorPorCurso(Guid cursoId, Guid pessoaId, FiltroPaginacao filtroPaginacao);
        
        /// <summary>
        /// Retorna o total de turmas de um curso de um professor para a paginação
        /// </summary>
        /// <param name="cursoId">Id curso</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPesquisa">Termo de pesquisa</param>
        /// <param name="ativo">Entidade ativa</param>
        /// <returns>Total de turmas</returns>
        Task<int> TotalTurmasProfessorPorCurso(Guid cursoId, Guid pessoaId, string filtroPesquisa, bool ativo = true);

        /// <summary>
        /// Retorna as turmas de um professor paginadas com filtro
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPaginacao">Filtros da paginação</param>       
        /// <returns>Lista de turmas</returns>
        Task<IEnumerable<Turma>> TurmasProfessor(Guid pessoaId, FiltroPaginacao filtroPaginacao);

        /// <summary>
        /// Retorna o total de turmas de um professor para a paginação
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPesquisa">Termo de pesquisa</param>
        /// <param name="ativo">Entidade ativa</param>
        /// <returns>Total de turmas/returns>
        Task<int> TotalTurmasProfessor(Guid pessoaId, string filtroPesquisa, bool ativo = true);

        /// <summary>
        /// Retorna todas as turmas de um aluno paginadas com filtro
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPaginacao">Filtros da paginação</param>       
        /// <returns>Lista de turmas</returns>
        Task<IEnumerable<Turma>> TurmasAluno(Guid pessoaId, FiltroPaginacao filtroPaginacao);

        /// <summary>
        /// Retorna o total de turmas de um professor para a paginação
        /// </summary>
        /// <param name="pessoaId">Id pessoa</param>
        /// <param name="filtroPesquisa">Termo de pesquisa</param>
        /// <param name="ativo">Entidade ativa</param>
        /// <returns>Total de turmas</returns>
        Task<int> TotalTurmasAluno(Guid pessoaId, string filtroPesquisa, bool ativo = true);
        
        /// <summary>
        /// Verifica se o professor é de uma determinada turma
        /// </summary>
        /// <param name="turmaId">Id instituição</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <returns></returns>
        Task<bool> ProfessorPertenceTurma(Guid turmaId, Guid pessoaId);

        /// <summary>
        /// Verifica se o aluno é de uma determinada turma
        /// </summary>
        /// <param name="turmaId">Id instituição</param>
        /// <param name="pessoaId">Id pessoa</param>
        /// <returns></returns>
        Task<bool> AlunoPertenceTurma(Guid turmaId, Guid pessoaId);

    }       

}

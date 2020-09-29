using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Utils.Comum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Pessoas
{
    public interface IAlunoRepository: IRepositoryBase<Aluno, Guid>
    {
        /// <summary>
        /// Retorna o aluno com todos os dados relacionados
        /// </summary>
        /// <param name="id">Id aluno</param>
        /// <returns></returns>
        Task<Aluno> ObterAlunoCompleto(Guid id);

        /// <summary>
        /// Retorna todos os alunos que estão no mesmo curso
        /// </summary>
        /// <param name="cursoId"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<IEnumerable<Aluno>> ObterAlunosPorCurso(Guid cursoId, FiltroPaginacao filtro);

        /// <summary>
        /// Retorna todos os alunos que estão em uma instituição 
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<IEnumerable<Aluno>> ObterAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro);

        /// <summary>
        /// Retorna o total de alunos por curso
        /// </summary>
        /// <param name="cursoId"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<int> TotalAlunosPorCurso(Guid cursoId, FiltroPaginacao filtro);

        /// <summary>
        /// Total de alunos por instituição
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<int> TotalAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro);
    }
}

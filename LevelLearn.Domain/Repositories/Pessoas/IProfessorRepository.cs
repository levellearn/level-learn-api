using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Utils.Comum;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Domain.Repositories.Pessoas
{
    public interface IProfessorRepository : IRepositoryBase<Professor, Guid>
    {
        /// <summary>
        /// Retorna o professor com todos os dados relacionados
        /// </summary>
        /// <param name="id">Id professor</param>
        /// <returns></returns>
        Task<Professor> ObterProfessorCompleto(Guid id);

        /// <summary>
        /// Retorna todos os professores que estão em uma instituição 
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<IEnumerable<Professor>> ObterProfessoresPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro);

        /// <summary>
        /// Total de professores por instituição
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        Task<int> TotalProfessoresPorInstituicao(Guid instituicaoId, FiltroPaginacao filtro);

    }
}

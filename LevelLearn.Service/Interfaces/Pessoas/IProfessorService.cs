using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Pessoas
{
    public interface IProfessorService : IServiceBase<Professor, Guid>, IDisposable
    {

        /// <summary>
        /// Retorna todos os professores por institução
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtroPaginacao"></param>
        /// <returns></returns>
        Task<ResultadoService<IEnumerable<Professor>>> ObterProfessorsPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao);

        /// <summary>
        /// Atualizar propriedades de um professor
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <param name="aluno"></param>
        /// <returns></returns>
        Task<ResultadoService> Atualizar(string usuarioId, Professor professor);
    }

}

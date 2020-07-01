using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Usuarios
{
    public interface IAlunoService: IServiceBase<Aluno, Guid>
    {
        /// <summary>
        /// Retorna todos os alunos por curso
        /// </summary>
        /// <param name="cursoId"></param>
        /// <param name="filtroPaginacao"></param>
        /// <returns></returns>
        Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorCurso(Guid cursoId, FiltroPaginacao filtroPaginacao);

        /// <summary>
        /// Retorna todos os alunos por institução
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtroPaginacao"></param>
        /// <returns></returns>
        Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao);

        /// <summary>
        /// Atualizar propriedades de um aluno
        /// </summary>
        /// <param name="aluno"></param>
        /// <returns></returns>
        Task<ResultadoService<Aluno>> Atualuzar(Aluno aluno);
    }

}

using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Pessoas;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Interfaces.Pessoas
{
    public interface IAlunoService : IPessoaService
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
        /// Atualizar campos do aluno por patch
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <param name="usuarioId"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        Task<ResultadoService> AtualizarPatch(Guid pessoaId, string usuarioId, JsonPatchDocument<AlunoAtualizaVM> patch);
    }

}

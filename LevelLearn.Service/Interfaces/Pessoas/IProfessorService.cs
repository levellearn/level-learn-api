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
    public interface IProfessorService : IPessoaService
    {

        /// <summary>
        /// Retorna todos os professores por institução
        /// </summary>
        /// <param name="instituicaoId"></param>
        /// <param name="filtroPaginacao"></param>
        /// <returns></returns>
        Task<ResultadoService<IEnumerable<Professor>>> ObterProfessorsPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao);

        /// <summary>
        /// Atualizar por patch Pessoa
        /// </summary>
        /// <param name="pessoaId"></param>
        /// <param name="usuarioId"></param>
        /// <param name="patch"></param>
        /// <returns></returns>
        Task<ResultadoService> AtualizarPatch(Guid pessoaId, string usuarioId, JsonPatchDocument<ProfessorAtualizaVM> patch);

    }

}

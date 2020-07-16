using AutoMapper;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Entities.Usuarios;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Pessoas;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Pessoas
{
    public class ProfessorService : PessoaService, IProfessorService
    {
        public ProfessorService(IUnitOfWork uow, ISharedResource sharedResource, UserManager<Usuario> userManager, IMapper mapper)
            : base(uow, sharedResource, userManager, mapper)
        {
        }

        public async Task<ResultadoService> AtualizarPatch(Guid pessoaId, string usuarioId, JsonPatchDocument<ProfessorAtualizaVM> patch)
        {
            Professor professorDb = await _uow.Professores.GetAsync(pessoaId);
            return await AtualizarPatch(professorDb, usuarioId, patch);
        }

        public async Task<ResultadoService<IEnumerable<Professor>>> ObterProfessorsPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Professor> professores = await _uow.Professores.ObterProfessoresPorInstituicao(instituicaoId, filtroPaginacao);
            int total = await _uow.Professores.TotalProfessoresPorInstituicao(instituicaoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Professor>>.Ok(professores, total);
        }


    }
}

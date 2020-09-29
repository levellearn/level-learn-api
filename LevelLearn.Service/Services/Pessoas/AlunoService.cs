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
    public class AlunoService : PessoaService, IAlunoService
    {
        public AlunoService(IUnitOfWork uow, ISharedResource sharedResource, UserManager<Usuario> userManager, IMapper mapper)
            : base(uow, sharedResource, userManager, mapper)
        {
        }

        public async Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Aluno> alunos = await _uow.Alunos.ObterAlunosPorInstituicao(instituicaoId, filtroPaginacao);
            int total = await _uow.Alunos.TotalAlunosPorInstituicao(instituicaoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(alunos, total);
        }

        public async Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorCurso(Guid cursoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Aluno> alunos = await _uow.Alunos.ObterAlunosPorCurso(cursoId, filtroPaginacao);
            int total = await _uow.Alunos.TotalAlunosPorCurso(cursoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(alunos, total);
        }

        public async Task<ResultadoService> AtualizarPatch(Guid pessoaId, string usuarioId, JsonPatchDocument<AlunoAtualizaVM> patch)
        {
            Aluno alunoDb = await _uow.Alunos.GetAsync(pessoaId);
            return await AtualizarPatch(alunoDb, usuarioId, patch);
        }
    }
}

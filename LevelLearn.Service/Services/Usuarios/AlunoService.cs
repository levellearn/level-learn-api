using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Usuarios;
using LevelLearn.Service.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Usuarios
{
    public class AlunoService : ServiceBase<Aluno, Guid>, IAlunoService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly PessoaResource _pessoaResource;

        public AlunoService(IUnitOfWork uow, ISharedResource sharedResource)
            : base(uow.Alunos)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _pessoaResource = PessoaResource.ObterInstancia();
        }

        public async Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorCurso(Guid cursoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Aluno> alunos = await _uow.Alunos.ObterAlunosPorCurso(cursoId, filtroPaginacao);
            int total = await _uow.Alunos.TotalAlunosPorCurso(cursoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(alunos, total);            
        }

        public async Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Aluno> alunos = await _uow.Alunos.ObterAlunosPorInstituicao(instituicaoId, filtroPaginacao);
            int total = await _uow.Alunos.TotalAlunosPorCurso(instituicaoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(alunos, total);
        }

        // TODO: Add JsonPatch
        public async Task<ResultadoService> Atualizar(Aluno aluno)
        {
            if (!aluno.EstaValido())
                return ResultadoServiceFactory.BadRequest(aluno.DadosInvalidos(), _sharedResource.DadosInvalidos);

            _uow.Alunos.Update(aluno);
            if (!await _uow.CommitAsync()) 
                return ResultadoServiceFactory.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}

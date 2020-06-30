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
            Task<IEnumerable<Aluno>> taskAlunos = _uow.Alunos.ObterAlunosPorCurso(cursoId, filtroPaginacao);
            Task<int> taskTotal = _uow.Alunos.TotalAlunosPorCurso(cursoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(await taskAlunos, await taskTotal);
        }

        public async Task<ResultadoService<IEnumerable<Aluno>>> ObterAlunosPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao)
        {
            Task<IEnumerable<Aluno>> taskAlunos = _uow.Alunos.ObterAlunosPorInstituicao(instituicaoId, filtroPaginacao);
            Task<int> taskTotal = _uow.Alunos.TotalAlunosPorCurso(instituicaoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Aluno>>.Ok(await taskAlunos, await taskTotal);
        }

        public async Task<ResultadoService<Aluno>> Atualuzar(Aluno aluno)
        {
            if (!aluno.EstaValido())
                return ResultadoServiceFactory<Aluno>.BadRequest(aluno.DadosInvalidos(), _sharedResource.DadosInvalidos);

            _uow.Alunos.Update(aluno);
            if (!await _uow.CommitAsync()) 
                return ResultadoServiceFactory<Aluno>.InternalServerError(_sharedResource.FalhaCadastrar);

            return ResultadoServiceFactory<Aluno>.Created(aluno, _sharedResource.CadastradoSucesso);
        }

    }
}

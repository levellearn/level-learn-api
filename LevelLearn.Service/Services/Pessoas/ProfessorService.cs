using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Pessoas;
using LevelLearn.Service.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Pessoas
{
    public class ProfessorService : ServiceBase<Professor, Guid>, IProfessorService
    {
        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly PessoaResource _pessoaResource;

        public ProfessorService(IUnitOfWork uow, ISharedResource sharedResource)
            : base(uow.Professores)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _pessoaResource = PessoaResource.ObterInstancia();
        }        

        public async Task<ResultadoService<IEnumerable<Professor>>> ObterProfessorsPorInstituicao(Guid instituicaoId, FiltroPaginacao filtroPaginacao)
        {
            IEnumerable<Professor> professores = await _uow.Professores.ObterProfessoresPorInstituicao(instituicaoId, filtroPaginacao);
            int total = await _uow.Professores.TotalProfessoresPorInstituicao(instituicaoId, filtroPaginacao);

            return ResultadoServiceFactory<IEnumerable<Professor>>.Ok(professores, total);
        }

        // TODO: Add JsonPatch
        public async Task<ResultadoService> Atualizar(Professor professor)
        {
            if (!professor.EstaValido())
                return ResultadoServiceFactory.BadRequest(professor.DadosInvalidos(), _sharedResource.DadosInvalidos);           

            _uow.Professores.Update(professor);
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

using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Utils.Comum;
using LevelLearn.Resource;
using LevelLearn.Resource.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class InstituicaoService : ServiceBase<Instituicao, Guid>, IInstituicaoService
    {
        #region Ctor

        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedResource;
        private readonly InstituicaoResource _resource;

        public InstituicaoService(IUnitOfWork uow, ISharedResource sharedResource)
            : base(uow.Instituicoes)
        {
            _uow = uow;
            _sharedResource = sharedResource;
            _resource = new InstituicaoResource();
        }

        #endregion

        public async Task<ResultadoService<Instituicao>> ObterInstituicao(Guid instituicaoId, Guid pessoaId)
        {
            Instituicao instituicao = await _uow.Instituicoes.InstituicaoCompleta(instituicaoId);

            if (instituicao == null)
                return ResultadoServiceFactory<Instituicao>.NotFound(_resource.InstituicaoNaoEncontrada);

            return ResultadoServiceFactory<Instituicao>.Ok(instituicao);
        }

        public async Task<ResultadoService<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(Guid pessoaId, FiltroPaginacao filtroPaginacao)
        {
            var instituicoes = await _uow.Instituicoes.InstituicoesProfessor(pessoaId, filtroPaginacao);

            var total = await _uow.Instituicoes.TotalInstituicoesProfessor(pessoaId, filtroPaginacao.FiltroPesquisa, filtroPaginacao.Ativo);

            return ResultadoServiceFactory<IEnumerable<Instituicao>>.Ok(instituicoes, total);
        }

        public async Task<ResultadoService<Instituicao>> CadastrarInstituicao(Instituicao instituicao, Guid pessoaId)
        {
            // Validação objeto
            if (!instituicao.EstaValido())
                return ResultadoServiceFactory<Instituicao>.BadRequest(instituicao.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            if (await InstituicaoExistente(instituicao))
                return ResultadoServiceFactory<Instituicao>.BadRequest(_resource.InstituicaoJaExiste);

            // Salva no BD
            var pessoaInstituicao = new PessoaInstituicao(PerfilInstituicao.ProfessorAdmin, pessoaId, instituicao.Id);
            instituicao.AtribuirPessoa(pessoaInstituicao);

            await _uow.Instituicoes.AddAsync(instituicao);
            if (!await _uow.CommitAsync())
                return ResultadoServiceFactory<Instituicao>.InternalServerError(_sharedResource.FalhaCadastrar);

            // Resposta Service
            return ResultadoServiceFactory<Instituicao>.Created(instituicao, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResultadoService<Instituicao>> EditarInstituicao(Guid instituicaoId, EditarInstituicaoVM instituicaoVM, Guid pessoaId)
        {
            var instituicaoExistente = await _uow.Instituicoes.GetAsync(instituicaoId);

            if (instituicaoExistente == null)
                return ResultadoServiceFactory<Instituicao>.NotFound(_resource.InstituicaoNaoEncontrada);

            // Modifica objeto
            instituicaoExistente.Atualizar(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            if (!instituicaoExistente.EstaValido())
                return ResultadoServiceFactory<Instituicao>.BadRequest(instituicaoExistente.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            bool professorAdmin = await _uow.Instituicoes.ProfessorAdmin(instituicaoId, pessoaId);

            if (!professorAdmin)
                return ResultadoServiceFactory<Instituicao>.Forbidden(_resource.InstituicaoNaoPermitida);

            if (await InstituicaoExistente(instituicaoExistente))
                return ResultadoServiceFactory<Instituicao>.BadRequest(_resource.InstituicaoJaExiste);

            // Salva no BD
            _uow.Instituicoes.Update(instituicaoExistente);

            if (!await _uow.CommitAsync())
                return ResultadoServiceFactory<Instituicao>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory<Instituicao>.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public async Task<ResultadoService<Instituicao>> AlternarAtivacao(Guid instituicaoId, Guid pessoaId)
        {
            // Validação BD
            Instituicao instituicao = await _uow.Instituicoes.GetAsync(instituicaoId);

            if (instituicao == null) return ResultadoServiceFactory<Instituicao>.NotFound(_resource.InstituicaoNaoEncontrada);

            bool professorAdmin = await _uow.Instituicoes.ProfessorAdmin(instituicaoId, pessoaId);
            if (!professorAdmin) return ResultadoServiceFactory<Instituicao>.Forbidden(_resource.InstituicaoNaoPermitida);

            if (instituicao.Ativo) instituicao.Desativar();
            else instituicao.Ativar();

            // Salva no BD
            _uow.Instituicoes.Update(instituicao);

            if (!await _uow.CommitAsync())
                return ResultadoServiceFactory<Instituicao>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResultadoServiceFactory<Instituicao>.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }


        private async Task<bool> InstituicaoExistente(Instituicao instituicao)
        {
            // Verifica se está tentando atualizar para uma instituição que já existe
            bool instituicaoExiste = await _uow.Instituicoes.EntityExists(i =>
                i.NomePesquisa == instituicao.NomePesquisa &&
                i.Id != instituicao.Id);

            return instituicaoExiste;
        }

    }
}

using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Resource;
using LevelLearn.Resource.Institucional;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class InstituicaoService : ServiceBase<Instituicao>, IInstituicaoService
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

        public async Task<ResponseAPI<Instituicao>> ObterInstituicao(Guid instituicaoId, Guid pessoaId)
        {
            Instituicao instituicao = await _uow.Instituicoes.InstituicaoCompleta(instituicaoId);

            if (instituicao == null)
                return ResponseFactory<Instituicao>.NotFound(_resource.InstituicaoNaoEncontrada);

            return ResponseFactory<Instituicao>.Ok(instituicao);
        }

        public async Task<ResponseAPI<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(Guid pessoaId, PaginationFilterVM filterVM)
        {
            string searchFilter = filterVM.SearchFilter;
            int pageNumber = filterVM.PageNumber;
            int pageSize = filterVM.PageSize;

            List<Instituicao> instituicoes =
                await _uow.Instituicoes.InstituicoesProfessor(pessoaId, searchFilter, pageNumber, pageSize);

            int total = await _uow.Instituicoes.TotalInstituicoesProfessor(pessoaId, searchFilter);

            return ResponseFactory<IEnumerable<Instituicao>>.Ok(instituicoes, total);
        }

        public async Task<ResponseAPI<Instituicao>> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM, Guid pessoaId)
        {
            var instituicao = new Instituicao(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            if (!instituicao.EstaValido())
                return ResponseFactory<Instituicao>.BadRequest(instituicao.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            if (await InstituicaoExistente(instituicao))
                return ResponseFactory<Instituicao>.BadRequest(_resource.InstituicaoJaExiste);

            // Salva no BD
            var pessoaInstituicao = new PessoaInstituicao(PerfisInstituicao.ProfessorAdmin, pessoaId, instituicao.Id);
            instituicao.AtribuirPessoa(pessoaInstituicao);

            await _uow.Instituicoes.AddAsync(instituicao);
            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_sharedResource.FalhaCadastrar);

            return ResponseFactory<Instituicao>.Created(instituicao, _sharedResource.CadastradoSucesso);
        }

        public async Task<ResponseAPI<Instituicao>> EditarInstituicao(Guid instituicaoId, EditarInstituicaoVM instituicaoVM, Guid pessoaId)
        {
            var instituicaoExistente = await _uow.Instituicoes.GetAsync(instituicaoId);

            if (instituicaoExistente == null)
                return ResponseFactory<Instituicao>.NotFound(_resource.InstituicaoNaoEncontrada);

            // Modifica objeto
            instituicaoExistente.Atualizar(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            if (!instituicaoExistente.EstaValido())
                return ResponseFactory<Instituicao>.BadRequest(instituicaoExistente.DadosInvalidos(), _sharedResource.DadosInvalidos);

            // Validação BD
            var isProfessorAdmin = await _uow.Instituicoes.IsProfessorAdmin(instituicaoId, pessoaId);

            if (!isProfessorAdmin)
                return ResponseFactory<Instituicao>.Forbidden(_resource.InstituicaoNaoPermitida);

            if (await InstituicaoExistente(instituicaoExistente))
                return ResponseFactory<Instituicao>.BadRequest(_resource.InstituicaoJaExiste);

            // Salva no BD
            _uow.Instituicoes.Update(instituicaoExistente);

            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_sharedResource.FalhaAtualizar);

            return ResponseFactory<Instituicao>.NoContent(_sharedResource.AtualizadoSucesso);
        }

        public async Task<ResponseAPI<Instituicao>> RemoverInstituicao(Guid instituicaoId, Guid pessoaId)
        {
            // Validação BD
            var isAdmin = await _uow.Instituicoes.IsProfessorAdmin(instituicaoId, pessoaId);

            if (!isAdmin)
                return ResponseFactory<Instituicao>.Forbidden(_resource.InstituicaoNaoPermitida);

            var instituicaoExistente = await _uow.Instituicoes.GetAsync(instituicaoId);

            if (instituicaoExistente == null)
                return ResponseFactory<Instituicao>.NotFound(_resource.InstituicaoNaoEncontrada);
          
            instituicaoExistente.Desativar();
            _uow.Instituicoes.Update(instituicaoExistente);

            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_sharedResource.FalhaDeletar);

            return ResponseFactory<Instituicao>.NoContent(_sharedResource.DeletadoSucesso);
        }

        private async Task<bool> InstituicaoExistente(Instituicao instituicao)
        {
            // Verifica se está tentando atualizar para uma instituição que já existe

            bool instituicaoExiste = await _uow.Instituicoes.EntityExists(i =>
                i.NomePesquisa == instituicao.NomePesquisa &&
                i.Id != instituicao.Id);

            return instituicaoExiste;
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}

using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Resource;
using LevelLearn.Service.Interfaces.Institucional;
using LevelLearn.Service.Response;
using LevelLearn.ViewModel;
using LevelLearn.ViewModel.Institucional.Instituicao;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class InstituicaoService : ServiceBase<Instituicao>, IInstituicaoService
    {
        private readonly IUnitOfWork _uow;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public InstituicaoService(IUnitOfWork uow, IStringLocalizer<SharedResource> sharedLocalizer)
            : base(uow.Instituicoes)
        {
            _uow = uow;
            _localizer = sharedLocalizer;
        }

        public async Task<ResponseAPI<Instituicao>> ObterInstituicao(Guid id, string pessoaId)
        {
            var instituicao = await _uow.Instituicoes.GetAsync(id);

            if (instituicao == null)
                return ResponseFactory<Instituicao>.NotFound(_localizer["InstitutionNotFound"]);

            return ResponseFactory<Instituicao>.Ok(instituicao);
        }

        public async Task<ResponseAPI<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(string pessoaId, PaginationQueryVM queryVM)
        {
            var instituicoes = await _uow.Instituicoes
                .InstituicoesProfessor(new Guid(pessoaId), queryVM.Query, queryVM.PageNumber, queryVM.PageSize);

            var total = await _uow.Instituicoes.TotalInstituicoesProfessor(new Guid(pessoaId), queryVM.Query);

            return ResponseFactory<IEnumerable<Instituicao>>.Ok(instituicoes, "", total);
        }

        public async Task<ResponseAPI<Instituicao>> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM, string pessoaId)
        {
            var instituicaoNova = new Instituicao(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            if (!instituicaoNova.EstaValido())
                return ResponseFactory<Instituicao>.BadRequest(instituicaoNova.DadosInvalidos());

            // Validação BD
            if (await _uow.Instituicoes.EntityExists(i => i.NomePesquisa == instituicaoNova.NomePesquisa))
                return ResponseFactory<Instituicao>.BadRequest(_localizer["InstitutionAlreadyExists"]);

            // Salva no BD
            var pessoaInstituicao = new PessoaInstituicao(PerfisInstituicao.ProfessorAdmin, new Guid(pessoaId), instituicaoNova.Id);
            instituicaoNova.AtribuirPessoa(pessoaInstituicao);

            await _uow.Instituicoes.AddAsync(instituicaoNova);
            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_localizer["InstitutionFailedSave"]);

            return ResponseFactory<Instituicao>.Created(instituicaoNova);
        }

        public async Task<ResponseAPI<Instituicao>> EditarInstituicao(Guid id, EditarInstituicaoVM instituicaoVM, string pessoaId)
        {
            // Validação BD
            var isAdmin = await _uow.Instituicoes.IsAdmin(id, new Guid(pessoaId));

            if (!isAdmin)
                return ResponseFactory<Instituicao>.Forbidden(_localizer["InstitutionNotAllowed"]);

            var instituicaoExistente = await _uow.Instituicoes.GetAsync(id);

            if (instituicaoExistente == null)
                return ResponseFactory<Instituicao>.NotFound(_localizer["InstitutionNotFound"]);

            // Verifica se está tentando atualizar uma instituição que já existe
            if (await _uow.Instituicoes.EntityExists(i =>
                    i.NomePesquisa == instituicaoVM.Nome.GenerateSlug() && i.Id != id)
                )
                return ResponseFactory<Instituicao>.BadRequest(_localizer["InstitutionAlreadyExists"]);

            // Modifica objeto
            instituicaoExistente.Atualizar(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            if (!instituicaoExistente.EstaValido())
                return ResponseFactory<Instituicao>.BadRequest(instituicaoExistente.DadosInvalidos());

            // Salva no BD
            _uow.Instituicoes.Update(instituicaoExistente);
            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_localizer["InstitutionFailedSave"]);

            return ResponseFactory<Instituicao>.NoContent();
        }

        public async Task<ResponseAPI<Instituicao>> RemoverInstituicao(Guid id, string pessoaId)
        {
            // Validação BD
            var isAdmin = await _uow.Instituicoes.IsAdmin(id, new Guid(pessoaId));

            if (!isAdmin)
                return ResponseFactory<Instituicao>.Forbidden(_localizer["InstitutionNotAllowed"]);

            var instituicaoExistente = await _uow.Instituicoes.GetAsync(id);

            if (instituicaoExistente == null)
                return ResponseFactory<Instituicao>.NotFound(_localizer["InstitutionNotFound"]);

            // TODO: Remover ou desativar?
            //_uow.Instituicoes.Remove(instituicaoExistente);

            instituicaoExistente.Desativar();
            _uow.Instituicoes.Update(instituicaoExistente);

            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_localizer["InstitutionFailedSave"]);

            return ResponseFactory<Instituicao>.NoContent();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}

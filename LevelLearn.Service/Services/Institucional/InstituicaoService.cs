using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.Domain.Validators;
using LevelLearn.Domain.Validators.Institucional;
using LevelLearn.Resource;
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
        private readonly IUnitOfWork _uow;
        private readonly ISharedResource _sharedLocalizer;
        private readonly IValidador<Instituicao> _validator;

        public InstituicaoService(IUnitOfWork uow, ISharedResource sharedLocalizer)
            : base(uow.Instituicoes)
        {
            _uow = uow;
            _sharedLocalizer = sharedLocalizer;
            _validator = new InstituicaoValidator(_sharedLocalizer);
        }

        public async Task<ResponseAPI<Instituicao>> ObterInstituicao(Guid id, string pessoaId)
        {
            var instituicao = await _uow.Instituicoes.GetAsync(id);

            if (instituicao == null)
                return ResponseFactory<Instituicao>.NotFound(_sharedLocalizer.InstituicaoNaoEncontrada);

            return ResponseFactory<Instituicao>.Ok(instituicao);
        }

        public async Task<ResponseAPI<IEnumerable<Instituicao>>> ObterInstituicoesProfessor(string pessoaId, PaginationQueryVM queryVM)
        {
            var instituicoes = await _uow.Instituicoes
                .InstituicoesProfessor(new Guid(pessoaId), queryVM.Query, queryVM.PageNumber, queryVM.PageSize);

            var total = await _uow.Instituicoes.TotalInstituicoesProfessor(new Guid(pessoaId), queryVM.Query);

            return ResponseFactory<IEnumerable<Instituicao>>
                .Ok(instituicoes, total, queryVM.PageNumber, queryVM.PageSize);
        }

        public async Task<ResponseAPI<Instituicao>> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM, string pessoaId)
        {
            var instituicaoNova = new Instituicao(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            _validator.Validar(instituicaoNova);

            if (!instituicaoNova.EstaValido())
                return ResponseFactory<Instituicao>.BadRequest(instituicaoNova.DadosInvalidos(), _sharedLocalizer.DadosInvalidos);

            // Validação BD
            if (await _uow.Instituicoes.EntityExists(i => i.NomePesquisa == instituicaoNova.NomePesquisa))
                return ResponseFactory<Instituicao>.BadRequest(_sharedLocalizer.InstituicaoJaExiste);

            // Salva no BD
            var pessoaInstituicao = new PessoaInstituicao(PerfisInstituicao.ProfessorAdmin, new Guid(pessoaId), instituicaoNova.Id);
            instituicaoNova.AtribuirPessoa(pessoaInstituicao);

            await _uow.Instituicoes.AddAsync(instituicaoNova);
            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_sharedLocalizer.FalhaCadastrar);

            return ResponseFactory<Instituicao>.Created(instituicaoNova, _sharedLocalizer.CadastradoSucesso);
        }

        public async Task<ResponseAPI<Instituicao>> EditarInstituicao(Guid id, EditarInstituicaoVM instituicaoVM, string pessoaId)
        {
            // Validação BD
            var isProfessorAdmin = await _uow.Instituicoes.IsAdmin(id, new Guid(pessoaId));

            if (!isProfessorAdmin)
                return ResponseFactory<Instituicao>.Forbidden(_sharedLocalizer.InstituicaoNaoPermitida);

            var instituicaoExistente = await _uow.Instituicoes.GetAsync(id);

            if (instituicaoExistente == null)
                return ResponseFactory<Instituicao>.NotFound(_sharedLocalizer.InstituicaoNaoEncontrada);

            // Verifica se está tentando atualizar uma instituição que já existe
            if (await _uow.Instituicoes.EntityExists(i =>
                    i.NomePesquisa == instituicaoVM.Nome.GenerateSlug() && i.Id != id)
                )
                return ResponseFactory<Instituicao>.BadRequest(_sharedLocalizer.InstituicaoJaExiste);

            // Modifica objeto
            instituicaoExistente.Atualizar(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            _validator.Validar(instituicaoExistente);

            if (!instituicaoExistente.EstaValido())
                return ResponseFactory<Instituicao>.BadRequest(instituicaoExistente.DadosInvalidos(), _sharedLocalizer.DadosInvalidos);

            // Salva no BD
            _uow.Instituicoes.Update(instituicaoExistente);
            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_sharedLocalizer.FalhaAtualizar);

            return ResponseFactory<Instituicao>.NoContent(_sharedLocalizer.AtualizadoSucesso);
        }

        public async Task<ResponseAPI<Instituicao>> RemoverInstituicao(Guid id, string pessoaId)
        {
            // Validação BD
            var isAdmin = await _uow.Instituicoes.IsAdmin(id, new Guid(pessoaId));

            if (!isAdmin)
                return ResponseFactory<Instituicao>.Forbidden(_sharedLocalizer.InstituicaoNaoPermitida);

            var instituicaoExistente = await _uow.Instituicoes.GetAsync(id);

            if (instituicaoExistente == null)
                return ResponseFactory<Instituicao>.NotFound(_sharedLocalizer.InstituicaoNaoEncontrada);

            // TODO: Remover ou desativar?
            //_uow.Instituicoes.Remove(instituicaoExistente);

            instituicaoExistente.Desativar();
            _uow.Instituicoes.Update(instituicaoExistente);

            if (!await _uow.CompleteAsync())
                return ResponseFactory<Instituicao>.InternalServerError(_sharedLocalizer.FalhaDeletar);

            return ResponseFactory<Instituicao>.NoContent(_sharedLocalizer.DeletadoSucesso);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}

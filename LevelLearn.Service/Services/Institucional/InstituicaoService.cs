using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Extensions;
using LevelLearn.Domain.Services;
using LevelLearn.Domain.Services.Institucional;
using LevelLearn.Domain.UnityOfWorks;
using LevelLearn.ViewModel.Institucional.Instituicao;
using System;
using System.Threading.Tasks;

namespace LevelLearn.Service.Services.Institucional
{
    public class InstituicaoService : ServiceBase<Instituicao>, IInstituicaoService
    {
        private readonly IUnitOfWork _uow;

        public InstituicaoService(IUnitOfWork uow)
            : base(uow.Instituicoes)
        {
            _uow = uow;
        }

        public async Task<ResponseAPI> CadastrarInstituicao(CadastrarInstituicaoVM instituicaoVM)
        {
            var instituicaoNova = new Instituicao(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            if (!instituicaoNova.EstaValido())
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", instituicaoNova.DadosInvalidos());
            
            // Validação BD
            if (await _uow.Instituicoes.EntityExists(i => i.NomePesquisa == instituicaoNova.NomePesquisa))
                return ResponseAPI.ResponseAPIFactory.BadRequest("Instituição já existente");

            // Salva no BD
            await _uow.Instituicoes.AddAsync(instituicaoNova);
            if (!await _uow.CompleteAsync()) return ResponseAPI.ResponseAPIFactory.InternalServerError("Falha ao salvar");

            return ResponseAPI.ResponseAPIFactory.Created(instituicaoNova);
        }

        public async Task<ResponseAPI> EditarInstituicao(Guid id, EditarInstituicaoVM instituicaoVM)
        {
            // Validação BD
            //verifica se está tentando atualizar uma instituição que já existe
            if (await _uow.Instituicoes.EntityExists(i => 
                    i.NomePesquisa == instituicaoVM.Nome.GenerateSlug() && i.Id != id)
                )
                return ResponseAPI.ResponseAPIFactory.BadRequest("Instituição já existente");

            var instituicaoExistente = await _uow.Instituicoes.GetAsync(id);

            if (instituicaoExistente == null) 
                return ResponseAPI.ResponseAPIFactory.NotFound("Instituição não existente");


            instituicaoExistente.Atualizar(instituicaoVM.Nome, instituicaoVM.Descricao);

            // Validação objeto
            if (!instituicaoExistente.EstaValido())
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", instituicaoExistente.DadosInvalidos());

            // Salva no BD
            _uow.Instituicoes.Update(instituicaoExistente);
            if (!await _uow.CompleteAsync()) return ResponseAPI.ResponseAPIFactory.InternalServerError("Falha ao salvar");

            return ResponseAPI.ResponseAPIFactory.NoContent();
        }

        public async Task<ResponseAPI> RemoverInstituicao(Guid id)
        {
            // Validação BD
            var instituicaoExistente = await _uow.Instituicoes.GetAsync(id);

            if (instituicaoExistente == null)
                return ResponseAPI.ResponseAPIFactory.NotFound("Instituição não existente");

            // TODO: Alguma regra de negócio?
            // TODO: Remover ou desativar?

            _uow.Instituicoes.Remove(instituicaoExistente);

            if (!await _uow.CompleteAsync()) return ResponseAPI.ResponseAPIFactory.InternalServerError("Falha ao salvar");

            return ResponseAPI.ResponseAPIFactory.NoContent();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}

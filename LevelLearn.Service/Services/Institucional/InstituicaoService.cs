using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Services;
using LevelLearn.Domain.Services.Institucional;
using LevelLearn.Domain.UnityOfWorks;
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

        public async Task<ResponseAPI> CadastrarInstituicao(Instituicao instituicao)
        {
            // Validação objeto
            if (!instituicao.EstaValido())
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", instituicao.DadosInvalidos());
            
            // Validação BD
            if (await _uow.Instituicoes.EntityExists(i => i.NomePesquisa == instituicao.NomePesquisa))
                return ResponseAPI.ResponseAPIFactory.BadRequest("Instituição já existente");

            // Salva no BD
            await _uow.Instituicoes.AddAsync(instituicao);
            if (!await _uow.CompleteAsync()) return ResponseAPI.ResponseAPIFactory.InternalServerError("Falha ao salvar");

            return ResponseAPI.ResponseAPIFactory.Created(instituicao);
        }
        public async Task<ResponseAPI> EditarInstituicao(Guid id, Instituicao instituicao)
        {
            // Validação objeto
            if (!instituicao.EstaValido())
                return ResponseAPI.ResponseAPIFactory.BadRequest("Dados inválidos", instituicao.DadosInvalidos());

            // Validação BD
            if (await _uow.Instituicoes.EntityExists(i => i.NomePesquisa == instituicao.NomePesquisa && i.Id != id))
                return ResponseAPI.ResponseAPIFactory.BadRequest("Instituição já existente");

            var instituicaoExistente = await _uow.Instituicoes.GetAsync(id);

            if (instituicaoExistente == null) 
                return ResponseAPI.ResponseAPIFactory.NotFound("Instituição não existente");


            instituicaoExistente.Atualizar(instituicao.Nome, instituicao.Descricao);

            // Salva no BD
            _uow.Instituicoes.Update(instituicaoExistente);
            if (!await _uow.CompleteAsync()) return ResponseAPI.ResponseAPIFactory.InternalServerError("Falha ao salvar");

            return ResponseAPI.ResponseAPIFactory.NoContent();
        }

        public void Dispose()
        {
            _uow.Dispose();
        }
        
    }
}

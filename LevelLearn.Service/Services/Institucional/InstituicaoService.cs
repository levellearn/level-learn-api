using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Services;
using LevelLearn.Domain.Services.Institucional;
using LevelLearn.Domain.UnityOfWorks;
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

            await _uow.Instituicoes.AddAsync(instituicao);
            await _uow.CompleteAsync();

            return ResponseAPI.ResponseAPIFactory.Created("Instituição cadastrada com sucesso", instituicao);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

    }
}

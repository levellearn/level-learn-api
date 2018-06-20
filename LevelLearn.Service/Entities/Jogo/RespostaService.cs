using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class RespostaService : CrudService<Resposta>, IRespostaService
    {
        private readonly IRespostaRepository _respostaRepository;
        public RespostaService(IRespostaRepository respostaRepository)
            : base(respostaRepository)
        {
            _respostaRepository = respostaRepository;
        }
    }
}

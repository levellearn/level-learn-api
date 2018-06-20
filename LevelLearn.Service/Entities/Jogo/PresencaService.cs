using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class PresencaService : CrudService<Presenca>, IPresencaService
    {
        private readonly IPresencaRepository _presencaRepository;
        public PresencaService(IPresencaRepository presencaRepository)
            : base(presencaRepository)
        {
            _presencaRepository = presencaRepository;
        }
    }
}

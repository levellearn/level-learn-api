using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class MoedaService : CrudService<Moeda>, IMoedaService
    {
        private readonly IMoedaRepository _moedaRepository;
        public MoedaService(IMoedaRepository moedaRepository)
            : base(moedaRepository)
        {
            _moedaRepository = moedaRepository;
        }
    }
}

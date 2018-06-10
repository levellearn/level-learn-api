using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Entities.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class MoedaService : CrudService<Moeda>, IMoedaService
    {
        public MoedaService(MoedaRepository moedaRepository)
            : base(moedaRepository)
        { }
    }
}

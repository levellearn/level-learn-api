using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class MissaoService : CrudService<Missao>, IMissaoService
    {
        private readonly IMissaoRepository _missaoRepository;
        public MissaoService(IMissaoRepository missaoRepository)
            : base(missaoRepository)
        {
            _missaoRepository = missaoRepository;
        }
    }
}

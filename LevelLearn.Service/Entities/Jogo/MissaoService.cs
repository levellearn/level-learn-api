using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Entities.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class MissaoService : CrudService<Missao>, IMissaoService
    {
        public MissaoService(MissaoRepository missaoRepository)
            : base(missaoRepository)
        { }
    }
}

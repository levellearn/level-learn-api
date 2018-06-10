using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Entities.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class PresencaService : CrudService<Presenca>, IPresencaService
    {
        public PresencaService(PresencaRepository presencaRepository)
            : base(presencaRepository)
        { }
    }
}

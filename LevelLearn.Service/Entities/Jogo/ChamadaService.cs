using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Entities.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class ChamadaService : CrudService<Chamada>, IChamadaService
    {
        public ChamadaService(ChamadaRepository chamadaRepository)
            : base(chamadaRepository)
        { }
    }
}

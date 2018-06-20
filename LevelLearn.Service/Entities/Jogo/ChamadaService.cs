using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class ChamadaService : CrudService<Chamada>, IChamadaService
    {
        private readonly IChamadaRepository _chamadaRepository;
        public ChamadaService(IChamadaRepository chamadaRepository)
            : base(chamadaRepository)
        {
            _chamadaRepository = chamadaRepository;
        }
    }
}

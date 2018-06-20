using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class TimeService : CrudService<Time>, ITimeService
    {
        private readonly ITimeRepository _timeRepository;
        public TimeService(ITimeRepository timeRepository)
            : base(timeRepository)
        {
            _timeRepository = timeRepository;
        }
    }
}

using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class AlunoTimeService : CrudService<AlunoTime>, IAlunoTimeService
    {
        private readonly IAlunoTimeRepository _alunoTimeRepository;
        public AlunoTimeService(IAlunoTimeRepository alunoTimeRepository)
            : base(alunoTimeRepository)
        {
            _alunoTimeRepository = alunoTimeRepository;
        }
    }
}

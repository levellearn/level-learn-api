using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Interfaces.Jogo;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Jogo;

namespace LevelLearn.Service.Entities.Jogo
{
    public class DesafioService : CrudService<Desafio>, IDesafioService
    {
        public DesafioService(IDesafioRepository desafiosRepository)
            : base(desafiosRepository)
        { }
    }
}

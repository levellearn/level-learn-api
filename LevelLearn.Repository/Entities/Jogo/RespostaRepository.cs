using LevelLearn.Domain.Jogo;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Jogo;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Jogo
{
    public class RespostaRepository : CrudRepository<Resposta>, IRespostaRepository
    {
        public RespostaRepository(DbContext context)
            : base(context)
        { }
    }
}

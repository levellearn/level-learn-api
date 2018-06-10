using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Base;
using LevelLearn.Repository.Interfaces.Pessoas;
using Microsoft.EntityFrameworkCore;

namespace LevelLearn.Repository.Entities.Pessoas
{
    public class NotificacaoRepository : CrudRepository<Notificacao>, INotificacaoRepository
    {
        public NotificacaoRepository(DbContext context)
            : base(context)
        { }
    }
}

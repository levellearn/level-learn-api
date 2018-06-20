using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class NotificacaoService : CrudService<Notificacao>, INotificacaoService
    {
        private readonly INotificacaoRepository _notificacaoRepository;
        public NotificacaoService(INotificacaoRepository notificacaoRepository)
            : base(notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository;
        }
    }
}

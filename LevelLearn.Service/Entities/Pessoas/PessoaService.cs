using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class PessoaService : CrudService<Pessoa>, IPessoaService
    {
        public PessoaService(IPessoaRepository pessoaRepository)
            : base(pessoaRepository)
        { }
    }
}

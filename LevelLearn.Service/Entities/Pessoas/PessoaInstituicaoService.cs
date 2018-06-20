using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class PessoaInstituicaoService : CrudService<PessoaInstituicao>, IPessoaInstituicaoService
    {
        private readonly IPessoaInstituicaoRepository _pessoaInstituicaoRepository;
        public PessoaInstituicaoService(IPessoaInstituicaoRepository pessoaInstituicaoRepository)
            : base(pessoaInstituicaoRepository)
        {
            _pessoaInstituicaoRepository = pessoaInstituicaoRepository;
        }
    }
}

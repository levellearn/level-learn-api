using System.Collections.Generic;
using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Pessoas;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Pessoas;

namespace LevelLearn.Service.Entities.Pessoas
{
    public class PessoaService : CrudService<Pessoa>, IPessoaService
    {
        private readonly IPessoaRepository _pessoaRepository;
        public PessoaService(IPessoaRepository pessoaRepository)
            : base(pessoaRepository)
        {
            _pessoaRepository = pessoaRepository;
        }

        public List<Pessoa> SelectAlunosCurso(int cursoId)
        {
            return _pessoaRepository.SelectAlunosCurso(cursoId);
        }

        public List<Pessoa> SelectAlunosInstituicao(int instituicaoId)
        {
            return _pessoaRepository.SelectAlunosInstituicao(instituicaoId);
        }

        public List<Pessoa> SelectProfessoresInstituicao(int instituicaoId)
        {
            return _pessoaRepository.SelectProfessoresInstituicao(instituicaoId);
        }
    }
}

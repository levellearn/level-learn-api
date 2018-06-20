using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Domain.Pessoas;
using LevelLearn.Repository.Interfaces.Institucional;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Institucional;
using System.Collections.Generic;
using System.Linq;

namespace LevelLearn.Service.Entities.Institucional
{
    public class InstituicaoService : CrudService<Instituicao>, IInstituicaoService
    {
        private readonly IInstituicaoRepository _instituicaoRepository;
        public InstituicaoService(IInstituicaoRepository instituicaoRepository)
            : base(instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }

        public bool Insert(Instituicao instituicao, List<int> admins, List<int> professores, List<int> alunos)
        {
            foreach (var item in admins)
            {
                instituicao.Pessoas.Add(new PessoaInstituicao
                {
                    InstituicaoId = instituicao.InstituicaoId,
                    Perfil = PerfilInstituicaoEnum.Admin,
                    PessoaId = item
                });
            }

            foreach (var item in professores)
            {
                if (instituicao.Pessoas.Where(p => p.PessoaId == item).Count() > 0)
                    continue;

                instituicao.Pessoas.Add(new PessoaInstituicao
                {
                    InstituicaoId = instituicao.InstituicaoId,
                    Perfil = PerfilInstituicaoEnum.Professor,
                    PessoaId = item
                });
            }

            foreach (var item in alunos)
            {
                if (instituicao.Pessoas.Where(p => p.PessoaId == item).Count() > 0)
                    continue;

                instituicao.Pessoas.Add(new PessoaInstituicao
                {
                    InstituicaoId = instituicao.InstituicaoId,
                    Perfil = PerfilInstituicaoEnum.Aluno,
                    PessoaId = item
                });
            }

            return _repository.Insert(instituicao);
        }

        public bool IsAdmin(int instituicaoId, int pessoaId)
        {
            return _instituicaoRepository.IsAdmin(instituicaoId, pessoaId);
        }

        public List<StatusResponseEnum> ValidaInstituicao(Instituicao instituicao)
        {
            List<StatusResponseEnum> valida = new List<StatusResponseEnum>();

            Instituicao validaInstituicao = new Instituicao();

            validaInstituicao = _repository.Select(p => p.Nome.ToUpper().Trim() == instituicao.Nome.ToUpper().Trim()).FirstOrDefault();

            if ((validaInstituicao != null) && (validaInstituicao.InstituicaoId != instituicao.InstituicaoId))
                valida.Add(StatusResponseEnum.NomeExistente);

            return valida;
        }
    }
}

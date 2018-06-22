using LevelLearn.Domain.Enum;
using LevelLearn.Domain.Institucional;
using LevelLearn.Service.Base;
using System.Collections.Generic;

namespace LevelLearn.Service.Interfaces.Institucional
{
    public interface IInstituicaoService : ICrudService<Instituicao>
    {
        List<StatusResponseEnum> ValidaInstituicao(Instituicao instituicao);
        List<Instituicao> InstituicoesAdmin(int pessoaId);
        List<Instituicao> InstituicoesProfessor(int pessoaId);
        List<Instituicao> InstituicoesAluno(int pessoaId);
        bool IsAdmin(int instituicaoId, int pessoaId);
        bool Insert(Instituicao instituicao, List<int> admins, List<int> professores, List<int> alunos);
    }
}

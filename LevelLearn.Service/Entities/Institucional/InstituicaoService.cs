using LevelLearn.Domain.Institucional;
using LevelLearn.Repository.Entities.Institucional;
using LevelLearn.Service.Base;
using LevelLearn.Service.Interfaces.Institucional;

namespace LevelLearn.Service.Entities.Institucional
{
    public class InstituicaoService : CrudService<Instituicao>, IInstituicaoService
    {
        public InstituicaoService(InstituicaoRepository instituicaoRepository)
            : base(instituicaoRepository)
        { }
    }
}

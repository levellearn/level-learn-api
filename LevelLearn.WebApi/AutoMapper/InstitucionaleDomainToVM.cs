using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.Domain.Entities.Pessoas;
using LevelLearn.Domain.Enums;
using LevelLearn.ViewModel.Enums;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.ViewModel.Pessoas;

namespace LevelLearn.WebApi.AutoMapper
{
    /// <summary>
    /// Institucional Domain To VM
    /// </summary>
    public class InstitucionalDomainToVM : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public InstitucionalDomainToVM()
        {
            CreateMap<Instituicao, InstituicaoVM>();
            CreateMap<Curso, CursoVM>();
            CreateMap<Pessoa, PessoaVM>();
            CreateMap<PerfisInstituicao, PerfisInstituicaoVM>();
            CreateMap<Generos, GenerosVM>();
            CreateMap<TiposPessoa, TiposPessoaVM>();
            CreateMap<PessoaInstituicao, PessoaInstituicaoVM>();

        }
    }
}

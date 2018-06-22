using AutoMapper;
using LevelLearn.Domain.Pessoas;
using LevelLearn.ViewModel.Pessoas.PessoaCurso;
using LevelLearn.ViewModel.Pessoas.PessoaInstituicao;

namespace LevelLearn.Web.AutoMapper
{
    public class PessoasDomainToViewModel : Profile
    {
        public PessoasDomainToViewModel()
        {
            CreateMap<PessoaInstituicao, ViewPessoaInstituicaoViewModel>();

            CreateMap<PessoaCurso, ViewPessoaCursoViewModel>();
        }
    }
}

using AutoMapper;
using LevelLearn.Domain.Institucional;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.ViewModel.Institucional.Turma;

namespace LevelLearn.Web.AutoMapper
{
    public class InstitucionalDomainToViewModel : Profile
    {
        public InstitucionalDomainToViewModel()
        {
            CreateMap<Instituicao, ViewInstituicaoViewModel>();
            CreateMap<Instituicao, UpdateInstituicaoViewModel>();

            CreateMap<Curso, ViewCursoViewModel>();
            CreateMap<Curso, UpdateCursoViewModel>();

            CreateMap<Turma, ViewTurmaViewModel>();
            CreateMap<Turma, UpdateTurmaViewModel>();
        }
    }
}

using AutoMapper;
using LevelLearn.Domain.Institucional;
using LevelLearn.ViewModel.Institucional.Curso;
using LevelLearn.ViewModel.Institucional.Instituicao;
using LevelLearn.ViewModel.Institucional.Turma;

namespace LevelLearn.Web.AutoMapper
{
    public class InstitucionalViewModelToDomain : Profile
    {
        public InstitucionalViewModelToDomain()
        {
            CreateMap<CreateInstituicaoViewModel, Instituicao>();
            CreateMap<UpdateInstituicaoViewModel, Instituicao>();

            CreateMap<CreateCursoViewModel, Curso>();
            CreateMap<UpdateCursoViewModel, Curso>();

            CreateMap<CreateTurmaViewModel, Turma>();
            CreateMap<UpdateTurmaViewModel, Turma>();
        }
    }
}

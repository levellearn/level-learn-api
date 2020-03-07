using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.WebApi.ViewModels.Institucional.Instituicao;

namespace LevelLearn.WebApi.AutoMapper
{
    public class InstitucionalVMToDomain : Profile
    {
        public InstitucionalVMToDomain()
        {
            CreateMap<InstituicaoVM, Instituicao>()
                .ConstructUsing(c =>
                    new Instituicao(c.Nome, c.Descricao)
                );

        }
    }
}

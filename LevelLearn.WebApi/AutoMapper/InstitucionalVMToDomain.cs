using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.ViewModel.Institucional.Instituicao;

namespace LevelLearn.WebApi.AutoMapper
{
    public class InstitucionalVMToDomain : Profile
    {
        public InstitucionalVMToDomain()
        {
            InstituicaoMap();

        }

        private void InstituicaoMap()
        {
            CreateMap<InstituicaoVM, Instituicao>()
                            .ConstructUsing(c =>
                                new Instituicao(c.Nome, c.Descricao)
                            );

            CreateMap<CadastrarInstituicaoVM, Instituicao>()
                .ConstructUsing(c =>
                    new Instituicao(c.Nome, c.Descricao)
                );

            CreateMap<EditarInstituicaoVM, Instituicao>()
               .ConstructUsing(c =>
                   new Instituicao(c.Nome, c.Descricao)
               );
        }
    }
}

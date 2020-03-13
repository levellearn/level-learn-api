﻿using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.ViewModel.Institucional.Instituicao;

namespace LevelLearn.WebApi.AutoMapper
{
    public class InstitucionalDomainToVM : Profile
    {
        public InstitucionalDomainToVM()
        {
            CreateMap<Instituicao, InstituicaoVM>();
            CreateMap<Curso, CursoVM>();

        }
    }
}

﻿using AutoMapper;
using LevelLearn.Domain.Entities.Institucional;
using LevelLearn.ViewModel.Institucional.Instituicao;

namespace LevelLearn.WebApi.AutoMapper
{
    /// <summary>
    /// Institucional VM To Domain
    /// </summary>
    public class InstitucionalVMToDomain : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public InstitucionalVMToDomain()
        {
            InstituicaoMap();
        }

        private void InstituicaoMap()
        {
            CreateMap<CadastrarInstituicaoVM, Instituicao>()
                .ConstructUsing(c =>
                    new Instituicao(c.Nome, c.Sigla, c.Descricao, c.Cnpj, c.OrganizacaoAcademica, c.Rede,
                        c.CategoriaAdministrativa, c.NivelEnsino, c.Cep, c.Municipio, c.UF)
                );
            
        }
    }
}

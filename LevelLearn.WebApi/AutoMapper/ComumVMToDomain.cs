﻿using AutoMapper;
using LevelLearn.Domain.Entities.Comum;
using LevelLearn.ViewModel;

namespace LevelLearn.WebApi.AutoMapper
{
    /// <summary>
    /// Comum VM To Domain
    /// </summary>
    public class ComumVMToDomain : Profile
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ComumVMToDomain()
        {
            ComumMap();
        }

        private void ComumMap()
        {
            CreateMap<PaginationFilterVM, FiltroPaginacao>()
                .ConstructUsing(p =>
                    new FiltroPaginacao(p.SearchFilter, p.PageNumber, p.PageSize, p.SortBy, p.AscendingSort, p.IsActive)
                );
        }

    }

}

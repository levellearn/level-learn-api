﻿using System;
using System.Collections.Generic;

namespace LevelLearn.ViewModel
{
    public class PaginatedListVM<T> where T : class
    {
        public PaginatedListVM(IEnumerable<T> data, int pageNumber, int pageSize, int total,
            string searchFilter, string sortBy, bool ascendingSort, bool isActive)
        {
            Data = data;
            PageNumber = pageNumber;
            PageSize = pageSize;
            Total = total;
            SearchFilter = searchFilter;
            SortBy = sortBy;
            AscendingSort = ascendingSort;
            IsActive = isActive;
        }

        public IEnumerable<T> Data { get; set; }

        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages { get => (int)Math.Ceiling((double)Total / PageSize); }
        public bool HasPreviousPage { get => (PageNumber > 1); }
        public bool HasNextPage { get => (PageNumber < TotalPages); }

        public string SearchFilter { get; set; }
        public string SortBy { get; set; }
        public bool AscendingSort { get; set; }
        public bool IsActive { get; set; }

    }
}

﻿using LevelLearn.ViewModel.Enums;
using System;

namespace LevelLearn.ViewModel
{
    public class ListBaseVM
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 1;
        public int Total { get; set; }
        public int TotalPages => CalcTotalPage();      
        public string SortBy { get; set; }
        public bool AscendingSort { get; set; }

        private int CalcTotalPage()
        {
            //if (Total == 0) return 0;

            Total = (Total <= 0) ? 1 : Total;
            PageSize = (PageSize <= 0) ? 1 : PageSize;

            return (int)Math.Ceiling((double)Total / PageSize);
        }

    }
}

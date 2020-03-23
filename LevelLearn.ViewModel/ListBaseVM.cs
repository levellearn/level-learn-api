﻿using System;

namespace LevelLearn.ViewModel
{
    public class ListBaseVM
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages => CalcTotalPage();

        private int CalcTotalPage()
        {
            //if (Total == 0) return 0;

            Total = (Total <= 0) ? 1 : Total;
            PageSize = (PageSize <= 0) ? 1 : PageSize;

            return (int)Math.Ceiling((double)Total / PageSize);
        }

    }
}

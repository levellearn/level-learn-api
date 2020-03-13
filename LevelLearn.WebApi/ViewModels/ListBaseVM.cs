using System;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class ListBaseVM
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
        public int TotalPages => CalcTotalPage();

        private int CalcTotalPage()
        {
            Total = (Total <= 0) ? 1 : Total;
            PageSize = (PageSize <= 0) ? 1 : PageSize;

            return (int)Math.Ceiling((double)Total / PageSize);
        }

    }
}

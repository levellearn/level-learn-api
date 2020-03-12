using System;
using System.Collections.Generic;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class InstituicaoListVM
    {
        public IEnumerable<InstituicaoVM> Data { get; set; }
        public int Total { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }        
        public int TotalPage => ((Total <= 0 ? 1 : Total) / (PageSize <= 0 ? 1 : PageSize));
        public string Query { get; set; }

    }
}

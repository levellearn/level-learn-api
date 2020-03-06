using System.Collections.Generic;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class InstituicaoListVM
    {
        public IEnumerable<InstituicaoVM> Instituicoes { get; set; }
        public int Count { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string Query { get; set; }

    }
}

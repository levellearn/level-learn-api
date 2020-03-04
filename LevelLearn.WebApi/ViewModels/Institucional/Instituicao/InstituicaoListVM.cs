using System.Collections.Generic;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class InstituicaoListVM
    {
        public IEnumerable<InstituicaoVM> Data { get; set; }
        public int Count { get; set; }
    }
}

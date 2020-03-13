using System.Collections.Generic;

namespace LevelLearn.WebApi.ViewModels.Institucional.Instituicao
{
    public class InstituicaoListVM : ListBaseVM
    {
        public IEnumerable<InstituicaoVM> Data { get; set; }

    }
}

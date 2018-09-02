using LevelLearn.ViewModel.Enum;
using LevelLearn.ViewModel.Pessoas.PessoaInstituicao;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class ViewInstituicaoViewModel
    {
        [DisplayName("Instituição")]
        public int InstituicaoId { get; set; }

        [DisplayName("Instituição")]
        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public PerfilInstituicaoEnumViewModel Perfil { get; set; }

        public List<ViewPessoaInstituicaoViewModel> Pessoas { get; set; } = new List<ViewPessoaInstituicaoViewModel>();
    }
}

using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel.Institucional.Instituicao
{
    public class CadastrarInstituicaoVM
    {
        /// <summary>
        /// Nome instituição
        /// </summary>
        /// 
        //[Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Descrição da instituição
        /// </summary>
        public string Descricao { get; set; }

    }
}

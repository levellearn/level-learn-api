namespace LevelLearn.ViewModel.Institucional.Curso
{
    public class CadastrarCursoVM
    {
        /// <summary>
        /// Nome do curso
        /// </summary>
        /// 
        //[Required(ErrorMessage = "Campo Nome é obrigatório")]
        public string Nome { get; set; }

        /// <summary>
        /// Sigla do curso
        /// </summary>
        public string Sigla { get; set; }

        /// <summary>
        /// Descrição do curso
        /// </summary>
        public string Descricao { get; set; }

    }
}

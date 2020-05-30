namespace LevelLearn.Domain.Entities.Comum
{
    public class FiltroPaginacao
    {
        public FiltroPaginacao(string filtroPesquisa, int numeroPagina, int tamanhoPorPagina, string ordenarPor, bool ordenacaoAscendente, bool ativo)
        {
            FiltroPesquisa = filtroPesquisa;
            NumeroPagina = numeroPagina;
            TamanhoPorPagina = tamanhoPorPagina;
            OrdenarPor = ordenarPor;
            OrdenacaoAscendente = ordenacaoAscendente;
            Ativo = ativo;
        }

        /// <summary>
        /// Termo pesquisa
        /// </summary>
        public string FiltroPesquisa { get; private set; }

        /// <summary>
        /// Número da página
        /// </summary>
        public int NumeroPagina { get; private set; } = 1;

        /// <summary>
        /// Quantidade de itens por página
        /// </summary>
        public int TamanhoPorPagina { get; private set; } = 1;

        /// <summary>
        /// Nome do campo para ordenação
        /// </summary>
        public string OrdenarPor { get; private set; }

        /// <summary>
        /// Tipo de ordenação
        /// </summary>
        public bool OrdenacaoAscendente { get; private set; } = true;

        /// <summary>
        /// Entidade ativa
        /// </summary>
        public bool Ativo { get; private set; } = true;
    }
}

namespace LevelLearn.Domain.Utils.Comum
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

        public string FiltroPesquisa { get; private set; }
        public int NumeroPagina { get; private set; } = 1;
        public int TamanhoPorPagina { get; private set; } = 1;
        public string OrdenarPor { get; private set; }
        public bool OrdenacaoAscendente { get; private set; } = true;
        public bool Ativo { get; private set; } = true;
    }
}

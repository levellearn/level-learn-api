namespace LevelLearn.Domain.Utils.Comum
{
    public class FiltroPaginacao
    {
        public string FiltroPesquisa { get; set; }
        public int NumeroPagina { get; set; } = 1;
        public int TamanhoPorPagina { get; set; } = 100;
        public string OrdenarPor { get; set; }
        public bool OrdenacaoAscendente { get; set; } = true;
        public bool Ativo { get; set; } = true;
    }
}

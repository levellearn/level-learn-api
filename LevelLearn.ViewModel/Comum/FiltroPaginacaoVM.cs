using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LevelLearn.ViewModel
{
    /// <summary>
    /// Classe utilizada para armezenar filtros de consulta
    /// </summary>
    public class FiltroPaginacaoVM
    {
        public FiltroPaginacaoVM()
        {
            NumeroPagina = 1;
            TamanhoPorPagina = 100;
            Ativo = true;
            OrdenacaoAscendente = true;
        }

        [JsonPropertyName("searchFilter")]
        public string FiltroPesquisa { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Intervalo inválido do número da página")]
        [JsonPropertyName("pageNumber")]
        public int NumeroPagina { get; set; }

        [Range(1, 100, ErrorMessage = "Intervalo inválido da quantidade de itens por página")]
        [JsonPropertyName("pageSize")]
        public int TamanhoPorPagina { get; set; }

        [JsonPropertyName("sortBy")]
        public string OrdenarPor { get; set; }

        [JsonPropertyName("ascendingSort")]
        public bool OrdenacaoAscendente { get; set; }

        [JsonPropertyName("isActive")]
        public bool Ativo { get; set; }

    }
}

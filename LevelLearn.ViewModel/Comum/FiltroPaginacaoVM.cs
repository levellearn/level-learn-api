using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LevelLearn.ViewModel
{
    //TODO: Corrigir nome "PaginationFilterVM"? JsonProperty
    /// <summary>
    /// Classe utilizada para armezenar filtros de consulta
    /// </summary>
    public class FiltroPaginacaoVM
    {
        public FiltroPaginacaoVM()
        {
            PageNumber = 1;
            PageSize = 100;
            IsActive = true;
            AscendingSort = true;
        }

        /// <summary>
        /// Termo pesquisa
        /// </summary>
        [JsonPropertyName("searchFilter")]
        public string SearchFilter { get; set; }

        /// <summary>
        /// Número da página
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Intervalo inválido do número da página")]
        [JsonPropertyName("pageNumber")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Quantidade de itens por página
        /// </summary>
        [Range(1, 100, ErrorMessage = "Intervalo inválido da quantidade de itens por página")]
        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }

        /// <summary>
        /// Nome do campo para ordenação
        /// </summary>
        [JsonPropertyName("sortBy")]
        public string SortBy { get; set; }

        /// <summary>
        /// Tipo de ordenação
        /// </summary>
        [JsonPropertyName("ascendingSort")]
        public bool AscendingSort { get; set; }

        /// <summary>
        /// Entidade ativa
        /// </summary>
        [JsonPropertyName("isActive")]
        public bool IsActive { get; set; }

    }
}

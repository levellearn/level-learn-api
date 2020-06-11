using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LevelLearn.ViewModel
{
    public class ListaPaginadaVM<T> where T : class
    {
        public ListaPaginadaVM(IEnumerable<T> data, int pageNumber, int pageSize, int total,
            string searchFilter, string sortBy, bool ascendingSort, bool isActive)
        {
            Dados = data;
            NumeroPagina = pageNumber;
            TamanhoPorPagina = pageSize;
            Total = total;
            FiltroPesquisa = searchFilter;
            OrdenarPor = sortBy;
            OrdenacaoAscendente = ascendingSort;
            Ativo = isActive;
        }

        [JsonPropertyName("data")]
        public IEnumerable<T> Dados { get; set; }

        [JsonPropertyName("pageNumber")]
        public int NumeroPagina { get; set; }

        [JsonPropertyName("pageSize")]
        public int TamanhoPorPagina { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("totalPages")]
        public int TotalPaginas { get => (int)Math.Ceiling((double)Total / TamanhoPorPagina); }

        [JsonPropertyName("hasPreviousPage")]
        public bool TemPaginaAnterior { get => (NumeroPagina > 1); }

        [JsonPropertyName("hasNextPage")]
        public bool TemProximaPagina { get => (NumeroPagina < TotalPaginas); }

        [JsonPropertyName("searchFilter")]
        public string FiltroPesquisa { get; set; }

        [JsonPropertyName("sortBy")]
        public string OrdenarPor { get; set; }

        [JsonPropertyName("ascendingSort")]
        public bool OrdenacaoAscendente { get; set; }

        [JsonPropertyName("isActive")]
        public bool Ativo { get; set; }

    }
}

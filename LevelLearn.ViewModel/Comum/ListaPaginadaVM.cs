using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LevelLearn.ViewModel
{
    public class ListaPaginadaVM<T> where T : class
    {
        public ListaPaginadaVM(IEnumerable<T> dados, int? total, FiltroPaginacaoVM filterVM)
        {
            Dados = dados;
            NumeroPagina = filterVM.NumeroPagina;
            TamanhoPorPagina = filterVM.TamanhoPorPagina;
            Total = total.Value;
            FiltroPesquisa = filterVM.FiltroPesquisa;
            OrdenarPor = filterVM.OrdenarPor;
            OrdenacaoAscendente = filterVM.OrdenacaoAscendente;
            Ativo = filterVM.Ativo;
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

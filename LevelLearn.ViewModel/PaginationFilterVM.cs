using System.ComponentModel.DataAnnotations;

namespace LevelLearn.ViewModel
{
    /// <summary>
    /// Classe utilizada para armezenar filtros de consulta
    /// </summary>
    public class PaginationFilterVM
    {
        public PaginationFilterVM()
        {
            PageNumber = 1;
            PageSize = 100;
            IsActive = true;
            AscendingSort = true;
        }

        public PaginationFilterVM(string searchFilter, int pageNumber, int pageSize, string sort, bool ascendingSort, bool isActive = true)
        {
            SearchFilter = searchFilter;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 1 : pageSize;
            SortBy = sort;
            AscendingSort = ascendingSort;
            IsActive = isActive;
        }

        /// <summary>
        /// Termo pesquisa
        /// </summary>
        public string SearchFilter { get; set; }

        /// <summary>
        /// Número da página
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Intervalo inválido do número da página")]
        public int PageNumber { get; set; }

        /// <summary>
        /// Quantidade de itens por página
        /// </summary>
        [Range(1, 100, ErrorMessage = "Intervalo inválido da quantidade de itens por página")]
        public int PageSize { get; set; }

        /// <summary>
        /// Nome do campo para ordenação
        /// </summary>
        public string SortBy { get; set; }

        /// <summary>
        /// Tipo de ordenação
        /// </summary>
        public bool AscendingSort { get; set; }

        /// <summary>
        /// Entidade ativa
        /// </summary>
        public bool IsActive { get; set; }

    }
}

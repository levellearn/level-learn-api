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
            SearchFilter = string.Empty;
            PageNumber = 1;
            PageSize = 100;
        }

        public PaginationFilterVM(string searchFilter, int pageNumber, int pageSize)
        {
            SearchFilter = searchFilter;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 1 : pageSize;
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
    }
}

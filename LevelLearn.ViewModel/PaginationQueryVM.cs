namespace LevelLearn.ViewModel
{
    /// <summary>
    /// Classe utilizada para armezenar filtros de consulta
    /// </summary>
    public class PaginationQueryVM
    {
        public PaginationQueryVM()
        {
            Query = string.Empty;
            PageNumber = 1;
            PageSize = 100;
        }

        public PaginationQueryVM(string query, int pageNumber, int pageSize)
        {
            Query = query;
            PageNumber = pageNumber <= 0 ? 1 : pageNumber;
            PageSize = pageSize <= 0 ? 1 : pageSize;
        }

        /// <summary>
        /// Termo pesquisa
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Número da página
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Quantidade de itens por página
        /// </summary>
        public int PageSize { get; set; }
    }
}

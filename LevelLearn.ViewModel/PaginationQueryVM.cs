namespace LevelLearn.ViewModel
{
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

        public string Query { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

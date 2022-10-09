namespace PersonnelManagement.Domain.Models
{
    public class PaginationQuery
    {
        public int PageNumber { get; }

        public int PageSize { get; }

        public PaginationQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}

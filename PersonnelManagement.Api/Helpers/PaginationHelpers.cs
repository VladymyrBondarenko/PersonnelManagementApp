using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Server.Services;

namespace PersonnelManagement.Server.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationQuery paginationQuery, List<T> response,
            int totalAmount)
        {
            var nextPage = paginationQuery.PageNumber >= 1
                ? uriService.GetAllDepartmentsUri(new PaginationQuery(paginationQuery.PageNumber + 1, paginationQuery.PageSize)).ToString()
                : null;

            var previousPage = paginationQuery.PageNumber - 1 >= 1
                ? uriService.GetAllDepartmentsUri(new PaginationQuery(paginationQuery.PageNumber - 1, paginationQuery.PageSize)).ToString()
                : null;

            return new PagedResponse<T>
            {
                Data = response,
                PageSize = paginationQuery.PageSize,
                PageNumber = paginationQuery.PageNumber,
                NextPage = nextPage,
                PreviousPage = previousPage,
                TotalAmount = totalAmount
            };
        }
    }
}

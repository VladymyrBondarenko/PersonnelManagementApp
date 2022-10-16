using PersonnelManagement.Contracts.v1.Responses;
using PersonnelManagement.Domain.Models;

namespace PersonnelManagement.Server.Services.PaginationServices
{
    public interface IPaginationService
    {
        PagedResponse<T> CreatePaginatedResponse<T>(PaginationQuery paginationQuery, List<T> response, int totalAmount);
    }
}

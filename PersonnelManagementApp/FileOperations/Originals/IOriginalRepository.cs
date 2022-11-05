using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Application.FileOperations.Originals
{
    public interface IOriginalRepository
    {
        Task<Original> CreateAsync(Original original);
        Task<bool> DeleteAsync(Original original);
        Task<List<Original>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllOriginalsFilter filter = null);
        Task<Original> GetAsync(Guid Id);
        Task<int> GetOriginalsAmountAsync(GetAllOriginalsFilter filter = null);
        Task<bool> UpdateOriginal(Original original);
    }
}
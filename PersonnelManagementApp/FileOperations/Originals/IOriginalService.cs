using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Application.FileOperations.Originals
{
    public interface IOriginalService
    {
        Task<Original> AddOriginalAsync(OriginalCreateParams createParams);
        Task<bool> DeleteOriginalAsync(Guid originalId);
        Task<Original> GetOriginalAsync(Guid id);
        Task<byte[]> GetOriginalBytesAsync(Guid id);
        Task<int> GetOriginalsAmountAsync(GetAllOriginalsFilter filter = null);
        Task<List<Original>> GetOriginalsAsync(PaginationQuery paginationFilter = null, GetAllOriginalsFilter filter = null);
        Task<bool> UpdateOriginal(Original original);
    }
}
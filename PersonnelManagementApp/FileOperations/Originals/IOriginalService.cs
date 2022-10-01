using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Application.FileOperations.Originals
{
    public interface IOriginalService
    {
        Task<Original> AddOriginalAsync(OriginalCreateParams createParams, OriginalType originalType);
        Task<bool> DeleteOriginalAsync(Original original);
        Task<Original> GetOriginalAsync(Guid id);
        Task<List<Original>> GetOriginalsAsync();
    }
}
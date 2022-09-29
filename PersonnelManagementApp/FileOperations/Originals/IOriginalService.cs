using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Application.FileOperations.Originals
{
    public interface IOriginalService
    {
        Task<Original> AddOriginalAsync(string sourceFilePath, OriginalType originalType, Guid bindedEntityKey = default);
        Task<bool> DeleteOriginalAsync(Original original);
        Task<Original> GetOriginalAsync(Guid id);
        Task<List<Original>> GetOriginalsAsync();
    }
}
using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Application.FileOperations.Originals
{
    public interface IOriginalRepository
    {
        Task<Original> CreateAsync(Original original);
        Task<bool> DeleteAsync(Original original);
        Task<List<Original>> GetAllAsync();
        Task<Original> GetAsync(Guid Id);
    }
}
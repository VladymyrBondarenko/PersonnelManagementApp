using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Positions;

namespace PersonnelManagement.Application.Positions
{
    public interface IPositionService
    {
        Task<Position> CreateAsync(Position position);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Position>> GetAllAsync();
        Task<List<Position>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllPositionsFilter filter = null);
        Task<Position> GetAsync(Guid Id);
        Task<int> GetPositionsAmountAsync();
        Task<bool> UpdateAsync(Position position);
    }
}
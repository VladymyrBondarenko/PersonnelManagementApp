using PersonnelManagement.Domain.Positions;

namespace PersonnelManagement.Application.Positions
{
    public interface IPositionService
    {
        Task<Position> CreateAsync(Position position);
        Task<Position> GetAsync(Guid Id);
    }
}
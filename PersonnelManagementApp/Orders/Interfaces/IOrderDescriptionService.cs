using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderDescriptionService
    {
        Task<OrderDescription> CreateAsync(OrderDescription order);
        Task<List<OrderDescription>> GetAllAsync();
        Task<OrderDescription> GetAsync(Guid id);
    }
}
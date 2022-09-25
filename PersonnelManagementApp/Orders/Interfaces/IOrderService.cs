using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<IOrderBase> CreateAsync(Order order);
        Task<List<IOrderBase>> GetAllAsync();
        Task<IOrderBase> GetOrderAsync(Guid id);
    }
}
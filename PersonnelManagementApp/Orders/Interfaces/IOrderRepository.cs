using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllOrdersFilter filter = null);
        Task<Order> GetOrderAsync(Guid id);
        Task<bool> UpdateAsync(Order order);
        Task<bool> DeleteAsync(Guid id);
        Task<int> GetOrdersAmountAsync(GetAllOrdersFilter filter = null);
    }
}
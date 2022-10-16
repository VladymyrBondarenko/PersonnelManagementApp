using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderDescriptionService
    {
        Task<OrderDescription> CreateAsync(OrderDescription order);
        Task<bool> DeleteAsync(Guid id);
        Task<List<OrderDescription>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllOrderDescriptionsFilter filter = null);
        Task<OrderDescription> GetAsync(Guid id);
        Task<int> GetOrderDescriptionsAmount();
        Task<bool> UpdateAsync(OrderDescription orderDesc);
    }
}
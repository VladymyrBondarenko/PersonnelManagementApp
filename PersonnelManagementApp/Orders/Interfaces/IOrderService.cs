using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<Original> AddOriginalAsync(OriginalCreateParams createParams);
        Task<IOrderBase> CreateAsync(Order order);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> DeleteOriginalAsync(OriginalDeleteParams deleteParams);
        Task<List<IOrderBase>> GetAllAsync();
        Task<List<IOrderBase>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllOrdersFilter filter = null);
        Task<IOrderBase> GetOrderAsync(Guid id);
        Task<int> GetOrdersAmountAsync(GetAllOrdersFilter filter = null);
        Task<bool> UpdateAsync(Order order);
    }
}
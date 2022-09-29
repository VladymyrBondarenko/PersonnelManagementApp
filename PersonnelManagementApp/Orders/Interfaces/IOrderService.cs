using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderService
    {
        Task<Original> AddOriginalAsync(OriginalCreateParams createParams);
        Task<IOrderBase> CreateAsync(Order order);
        Task<bool> DeleteOriginalAsync(OriginalDeleteParams deleteParams);
        Task<List<IOrderBase>> GetAllAsync();
        Task<IOrderBase> GetOrderAsync(Guid id);
    }
}
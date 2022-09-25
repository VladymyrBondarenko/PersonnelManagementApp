using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderFactory
    {
        IOrderBase GetOrder(Order order);
    }
}
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Infrastracture.Orders.OrderBase.Models
{
    internal class OtherOrder : IOrderBase
    {
        public Order Order => throw new NotImplementedException();

        public Task<bool> AcceptOrderAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RollbackOrderAsync(bool toProject = false)
        {
            throw new NotImplementedException();
        }
    }
}

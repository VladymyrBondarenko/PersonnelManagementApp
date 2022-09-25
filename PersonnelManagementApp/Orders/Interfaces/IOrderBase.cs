using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;

namespace PersonnelManagement.Application.Orders.Interfaces
{
    public interface IOrderBase
    {
        Task<bool> AcceptOrderAsync();
        Task<bool> RollbackOrderAsync(bool toProject = false);
        Order Order { get; }
    }
}

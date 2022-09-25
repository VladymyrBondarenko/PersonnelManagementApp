using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;

namespace PersonnelManagement.Application.Employees
{
    public interface IEmployeeService
    {
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> CreateAsync(IOrderBase order);
        Task<bool> DeleteAsync(Employee employee);
        Task<Employee> GetAsync(Guid id);
        Task<bool> UpdateAsync(Employee employee);
    }
}
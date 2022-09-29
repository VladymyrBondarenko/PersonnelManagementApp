using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Application.Employees
{
    public interface IEmployeeService
    {
        Task<Original> AddOriginalAsync(OriginalCreateParams createParams);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> CreateAsync(IOrderBase order);
        Task<bool> DeleteAsync(Employee employee);
        Task<bool> DeleteOriginalAsync(OriginalDeleteParams deleteParams);
        Task<Employee> GetAsync(Guid id);
        Task<bool> UpdateAsync(Employee employee);
    }
}
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Application.Employees
{
    public interface IEmployeeService
    {
        Task<Original> AddOriginalAsync(OriginalCreateParams createParams);
        Task<Employee> CreateAsync(Employee employee);
        Task<Employee> CreateAsync(IOrderBase order);
        Task<bool> DeleteAsync(Guid employeeId);
        Task<bool> DeleteOriginalAsync(OriginalDeleteParams deleteParams);
        Task<List<Employee>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllEmployeesFilter filter = null);
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetAsync(Guid id);
        Task<int> GetEmployeesAmountAsync();
        Task<bool> UpdateAsync(Employee employee);
    }
}
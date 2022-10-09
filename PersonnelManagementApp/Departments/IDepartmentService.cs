using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;

namespace PersonnelManagement.Application.Departments
{
    public interface IDepartmentService
    {
        Task<Department> CreateAsync(Department department);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Department>> GetAllAsync();
        Task<List<Department>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllDepartmentsFilter filter = null);
        Task<Department> GetAsync(Guid Id);
        Task<int> GetDepartmentsAmountAsync();
        Task<bool> UpdateAsync(Department department);
    }
}
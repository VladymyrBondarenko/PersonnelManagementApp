using PersonnelManagement.Domain.Departments;

namespace PersonnelManagement.Application.Departments
{
    public interface IDepartmentService
    {
        Task<Department> CreateAsync(Department department);
        Task<bool> DeleteAsync(Guid id);
        Task<List<Department>> GetAllAsync();
        Task<Department> GetAsync(Guid Id);
        Task<bool> UpdateAsync(Department department);
    }
}
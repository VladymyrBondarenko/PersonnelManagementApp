using PersonnelManagement.Domain.Departments;

namespace PersonnelManagement.Application.Departments
{
    public interface IDepartmentService
    {
        Task<Department> CreateAsync(Department department);
        Task<Department> GetAsync(Guid Id);
    }
}
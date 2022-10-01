using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models.Originals;

namespace PersonnelManagement.Infrastracture.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IApplicationDbContext _dbContext;
        private readonly IOriginalService _originalService;

        public EmployeeService(IApplicationDbContext dbContext, IOriginalService originalService)
        {
            _dbContext = dbContext;
            _originalService = originalService;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            var addedEmployee = await _dbContext.Employees.AddAsync(employee);

            await _dbContext.SaveChangesAsync();

            return addedEmployee.Entity;
        }

        public async Task<Employee> CreateAsync(IOrderBase order)
        {
            if(order?.Order == null) 
            {
                return null;
            }

            var newEmployee = new Employee();
            newEmployee.FirstName = order.Order.FirstName;
            newEmployee.LastName = order.Order.LastName;
            newEmployee.HireDate = order.Order.DateFrom;
            newEmployee.DepartmentId = order.Order.DepartmentId;
            newEmployee.PositionId = order.Order.PositionId;
            newEmployee.EmployeeState = EmployeeState.Hired;

            var addedEmployee = await _dbContext.Employees.AddAsync(newEmployee);

            await _dbContext.SaveChangesAsync();

            return addedEmployee.Entity;
        }

        public async Task<bool> UpdateAsync(Employee employee)
        {
            var exists = _dbContext.Employees.Any(x => x.Id == employee.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.Employees.Update(employee);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Employee employee)
        {
            var exists = _dbContext.Employees.Any(x => x.Id == employee.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.Employees.Remove(employee);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<Employee> GetAsync(Guid id)
        {
            return await _dbContext.Employees
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.Orders)
                .Include(x => x.Originals)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Original> AddOriginalAsync(OriginalCreateParams createParams)
        {
            var employee = await GetAsync(createParams.EmployeeId);

            if (employee == null)
            {
                return null;
            }

            var original = await _originalService.AddOriginalAsync(createParams, OriginalType.Employees);
            return original;
        }

        public async Task<bool> DeleteOriginalAsync(OriginalDeleteParams deleteParams)
        {
            var original = await _originalService.GetOriginalAsync(deleteParams.OriginalId);

            if (original == null || original.EmployeeId != deleteParams.EmployeeId)
            {
                return false;
            }

            var origDeleted = await _originalService.DeleteOriginalAsync(original);
            return origDeleted;
        }
    }
}
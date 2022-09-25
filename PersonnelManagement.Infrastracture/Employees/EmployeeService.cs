using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;

namespace PersonnelManagement.Infrastracture.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IApplicationDbContext _dbContext;

        public EmployeeService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            var addedEmployee = await _dbContext.Employees.AddAsync(employee);
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
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
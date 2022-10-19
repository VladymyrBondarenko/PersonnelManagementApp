using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Employees;
using PersonnelManagement.Application.FileOperations.Originals;
using PersonnelManagement.Application.Orders.Interfaces;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
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

        public async Task<int> GetEmployeesAmountAsync()
        {
            return await _dbContext.Employees.CountAsync();
        }

        public async Task<List<Employee>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllEmployeesFilter filter = null)
        {
            var queryable = _dbContext.Employees
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.Orders)
                .Include(x => x.Originals).AsQueryable();

            if (filter != null)
            {
                queryable = addFiltersOnQuery(filter, queryable);
            }

            if (paginationFilter == null || paginationFilter.PageSize < 1 || paginationFilter.PageNumber < 1)
            {
                return await queryable.ToListAsync();
            }

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            return await queryable
                .Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _dbContext.Employees
                .Include(x => x.Department)
                .Include(x => x.Position)
                .Include(x => x.Orders)
                .Include(x => x.Originals).ToListAsync();
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

        public async Task<bool> DeleteAsync(Guid employeeId)
        {
            var employee = await _dbContext.Employees.FindAsync(employeeId);

            if (employee == null)
            {
                return false;
            }

            _dbContext.Employees.Remove(employee);

            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
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

        private static IQueryable<Employee> addFiltersOnQuery(GetAllEmployeesFilter filter, IQueryable<Employee> queryable)
        {
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var text = filter.SearchText;
                queryable = queryable.Where(x =>
                    x.FirstName.Contains(text) ||
                    x.LastName.Contains(text) ||
                    (x.Department != null ? x.Department.DepartmentTitle.Contains(text) : false) ||
                    (x.Position != null ? x.Position.PositionTitle.Contains(text) : false));
            }

            return queryable;
        }
    }
}
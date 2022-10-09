using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Models;
using PersonnelManagement.Domain.Models.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Infrastracture.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IApplicationDbContext _dbContext;

        public DepartmentService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetDepartmentsAmountAsync()
        {
            return await _dbContext.Departments.CountAsync();
        }

        public async Task<List<Department>> GetAllAsync(PaginationQuery paginationFilter = null, GetAllDepartmentsFilter filter = null)
        {
            var queryable = _dbContext.Departments.AsQueryable();

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

        public async Task<List<Department>> GetAllAsync()
        {
            return await _dbContext.Departments.ToListAsync();
        }

        public async Task<Department> GetAsync(Guid Id)
        {
            return await _dbContext.Departments.FindAsync(Id);
        }

        public async Task<Department> CreateAsync(Department department)
        {
            await _dbContext.Departments.AddAsync(department);
            var added = await _dbContext.SaveChangesAsync() > 0;

            if (added)
            {
                var newDepartment = await GetAsync(department.Id);
                return newDepartment;
            }

            return null;
        }

        public async Task<bool> UpdateAsync(Department department)
        {
            var exists = _dbContext.Departments.Any(x => x.Id == department.Id);

            if (!exists)
            {
                return false;
            }

            _dbContext.Departments.Update(department);

            return await _dbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var department = await _dbContext.Departments.FindAsync(id);

            if (department == null)
            {
                return false;
            }

            _dbContext.Departments.Remove(department);

            try
            {
                return await _dbContext.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        private static IQueryable<Department> addFiltersOnQuery(GetAllDepartmentsFilter filter, IQueryable<Department> queryable)
        {
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var text = filter.SearchText;
                queryable = queryable.Where(x =>
                    x.DepartmentTitle.Contains(text) ||
                    x.DepartmentDescription.Contains(text) ||
                    x.DateFrom.ToString().Contains(text) ||
                    x.DateTo.ToString().Contains(text));
            }

            return queryable;
        }
    }
}

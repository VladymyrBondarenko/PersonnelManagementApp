using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Application.Departments;
using PersonnelManagement.Domain.Departments;
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
    }
}

using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Application.DbContexts
{
    public interface IApplicationDbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDescription> OrdersDescription { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Original> Originals { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

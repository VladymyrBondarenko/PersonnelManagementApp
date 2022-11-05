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

namespace PersonnelManagement.Infrastracture.DbContexts
{
    public partial class ApplicationDbContext : DbContext
    {
        protected void ConfigureDefaultValues(ModelBuilder modelBuilder)
        {
            configurateDepartmentDefaults(modelBuilder);
            configuratePositionDefaults(modelBuilder);
            configurateOrderDefaults(modelBuilder);
            configurateEmployeeDefaults(modelBuilder);
            configurateOriginalDefaults(modelBuilder);
        }

        private void configurateDepartmentDefaults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(b => b.CreatedDate)
                .HasDefaultValue(DateTime.UtcNow);
        }

        private void configuratePositionDefaults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Position>()
                .Property(b => b.CreatedDate)
                .HasDefaultValue(DateTime.UtcNow);
        }

        private void configurateOrderDefaults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .Property(b => b.CreatedDate)
                .HasDefaultValue(DateTime.UtcNow);
        }

        private void configurateEmployeeDefaults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(b => b.CreatedDate)
                .HasDefaultValue(DateTime.UtcNow);
        }

        private void configurateOriginalDefaults(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Original>()
                .Property(b => b.CreatedDate)
                .HasDefaultValue(DateTime.UtcNow);
        }
    }
}

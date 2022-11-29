using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public partial class ApplicationDbContext : IdentityDbContext
    {
        protected void ConfigureDefaultValues(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Position>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Order>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Employee>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<Original>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<OrderDescription>()
               .Property(b => b.CreatedDate)
               .HasDefaultValueSql("getdate()");
        }
    }
}

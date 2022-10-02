using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Infrastracture.DbContexts
{
    public partial class ApplicationDbContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            configureEmployeesRelations(modelBuilder);

            //configureOrderDescriptionsRelations(modelBuilder);

            configureOrdersRelations(modelBuilder);
        }

        private void configureEmployeesRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
               .HasMany(x => x.Orders)
               .WithOne(x => x.Employee)
               .HasForeignKey(x => x.EmployeeId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Department)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasOne(x => x.Position)
                .WithMany(x => x.Employees)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Employee>()
                .HasMany(x => x.Originals)
                .WithOne(x => x.Employee)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void configureOrdersRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(x => x.OrderDescription)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.OrderDescriptionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
               .HasOne(x => x.Department)
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Position)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.PositionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasOne(x => x.Employee)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(x => x.Originals)
                .WithOne(x => x.Order)
                .HasForeignKey(x => x.OrderId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

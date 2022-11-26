using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models.Identity;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;

namespace PersonnelManagement.Infrastracture.DbContexts
{
    public partial class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderDescription> OrdersDescription { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Position> Positions { get; set; }

        public DbSet<Original> Originals { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}

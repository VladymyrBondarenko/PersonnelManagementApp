using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Application.DbContexts;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;

namespace PersonnelManagement.Infrastracture.DbContexts
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureRelations(modelBuilder);

            ConfigureDefaultValues(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
    }
}

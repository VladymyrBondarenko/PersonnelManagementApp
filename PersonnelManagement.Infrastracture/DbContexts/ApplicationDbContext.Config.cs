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
            ConfigureRelations(modelBuilder);

            ConfigureDefaultValues(modelBuilder);
        }
    }
}

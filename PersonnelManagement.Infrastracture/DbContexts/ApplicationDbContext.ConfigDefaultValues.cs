using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Domain.Departments;
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
    }
}

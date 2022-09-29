using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PersonnelManagement.Api.Data
{
    public class IdentitiesDbContext : IdentityDbContext
    {
        public IdentitiesDbContext(DbContextOptions<IdentitiesDbContext> options)
            : base(options)
        {
        }
    }
}
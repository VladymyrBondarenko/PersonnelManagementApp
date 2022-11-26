using Microsoft.AspNetCore.Identity;
using PersonnelManagement.Domain.Employees;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Models.Identity
{
    public class IdentityUserModel : IdentityUser
    {
        public Guid? EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [AllowNull()]
        public Employee Employee { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}

using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Departments
{
    public class Department
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string DepartmentTitle { get; set; }

        [MaxLength(300)]
        public string DepartmentDescription { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [AllowNull]
        public DateTime DateTo { get; set; }

        public DateTime CreatedDate { get; set; }

        public ICollection<Order> Orders { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}

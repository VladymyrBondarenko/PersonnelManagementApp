using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Orders;
using PersonnelManagement.Domain.Positions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Employees
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime FireDate { get; set; }

        //public Guid HireOrderId { get; set; }

        //[ForeignKey(nameof(HireOrderId))]
        //public Order HireOrder { get; set; }

        //public Guid FireOrderId { get; set; }

        //[ForeignKey(nameof(FireOrderId))]
        //public Order FireOrder { get; set; }

        public ICollection<Order> Orders { get; set; }

        public Guid? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [AllowNull]
        public Department Department { get; set; }

        public Guid? PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]
        [AllowNull]
        public Position Position { get; set; }

        public EmployeeState EmployeeState { get; set; }
    }
}
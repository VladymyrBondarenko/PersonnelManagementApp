using PersonnelManagement.Domain.Departments;
using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Models.Originals;
using PersonnelManagement.Domain.Positions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Orders
{
    public class Order
    {
        [Key]
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string FirstName { get; set; }

        [MaxLength(100)]
        public string LastName { get; set; }

        public Guid? EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [AllowNull()]
        public Employee Employee { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Guid? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        [AllowNull()]
        public Department Department { get; set; }

        public Guid? PositionId { get; set; }

        [ForeignKey(nameof(PositionId))]
        [AllowNull()]
        public Position Position { get; set; }

        public Guid OrderDescriptionId { get; set; }

        [ForeignKey(nameof(OrderDescriptionId))]
        public OrderDescription OrderDescription { get; set; }

        public OrderState OrderState { get; set; }

        public ICollection<Original> Originals { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
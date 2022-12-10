using PersonnelManagement.Domain.Employees;
using PersonnelManagement.Domain.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Domain.Models.Originals
{
    public class Original
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string OriginalTitle { get; set; }

        [MaxLength(120)]
        public string FileName { get; set; }

        public string OriginalPath { get; set; }

        public string OriginalFileExtension { get; set; }

        public Guid? OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        [AllowNull]
        public Order Order { get; set; }

        public Guid? EmployeeId { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [AllowNull]
        public Employee Employee { get; set; }

        public OriginalType OriginalType { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}

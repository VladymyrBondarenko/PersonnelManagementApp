using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    }
}

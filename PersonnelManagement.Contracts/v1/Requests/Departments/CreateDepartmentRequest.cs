using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests
{
    public class CreateDepartmentRequest
    {
        public string DepartmentTitle { get; set; }

        public string DepartmentDescription { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }
    }
}

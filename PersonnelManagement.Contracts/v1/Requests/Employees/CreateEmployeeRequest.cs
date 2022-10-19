using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Employees
{
    public class CreateEmployeeRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime HireDate { get; set; }

        public DateTime FireDate { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PositionId { get; set; }
    }
}

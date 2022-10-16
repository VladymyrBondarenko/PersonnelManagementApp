using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Contracts.v1.Requests.Orders
{
    public class UpdateOrderRequest
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Guid? DepartmentId { get; set; }

        public Guid? PositionId { get; set; }
    }
}
